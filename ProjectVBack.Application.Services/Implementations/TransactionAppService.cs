using AutoMapper;
using FluentValidation;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Crosscutting.CustomExceptions;
using ProjectVBack.Crosscutting.Utils;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Domain.Repositories.Abstractions;

namespace ProjectVBack.Application.Services;

public class TransactionAppService : ITransactionAppService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<AddTransactionRequest> _addTransactionValidator;
    private readonly IValidator<EditTransactionRequest> _editTransactionValidator;
    private const int SummaryRound = 2;


    public TransactionAppService(IUnitOfWork unitOfWork, IMapper mapper
        ,IValidator<AddTransactionRequest> addTransactionRequestValidator
        ,IValidator<EditTransactionRequest> editTransactionRequestValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _addTransactionValidator = addTransactionRequestValidator;
        _editTransactionValidator = editTransactionRequestValidator;
    }

    public async Task<IEnumerable<TransactionCategoryDto>> GetAllTransactionsWithCategoryInfo(string userId, GetTransactionsRequest dto)
    {
        var transactions = await _unitOfWork.Transactions.GetFiltered(userId, dto.From, dto.To);

        var categoryIds = transactions.Select(transaction => transaction.CategoryId);

        var categories = await _unitOfWork.Categories.GetFiltered(categoryIds, dto.CategoryType);

        var filteredcategoryIds = categories.Select(category => category.Id);

        var transactionsGroupByCategoryId = transactions
            .Where(transaction => filteredcategoryIds.Contains(transaction.CategoryId))
            .GroupBy(transaction => transaction.CategoryId);

        var responseDto = new List<TransactionCategoryDto>();

        foreach (var transactionGroup in transactionsGroupByCategoryId)
        {
            var category = categories.First(category => category.Id == transactionGroup.Key);

            var transactionCategoryDtos = transactionGroup.Select(transaction =>
                new TransactionCategoryDto(
                    transaction.Id,
                    transaction.Description,
                    transaction.Value,
                    transaction.Date,
                    category.Type.ToString(),
                    category.Name,
                    category.PictureUrl,
                    category.IsDefault)
                );

            responseDto.AddRange(transactionCategoryDtos);
        }

        return responseDto;
    }

    public async Task<TransactionsSummaryDto> GetSummary(string userId, GetTransactionsSummaryRequest dto)
    {
        var transactions = await _unitOfWork.Transactions.GetFiltered(userId, dto.From, dto.To);

        var categoryIds = transactions.Select(transaction => transaction.CategoryId);

        var categories = await _unitOfWork.Categories.GetFiltered(categoryIds);

        var incomeTransactionsSum = GetTransactionsSumByCategoryType(transactions, categories, CategoryType.Income);
        var expenseTransactionsSum = GetTransactionsSumByCategoryType(transactions, categories, CategoryType.Expense);

        double incomes = Math.Round(incomeTransactionsSum, SummaryRound);
        double expenses = Math.Round(expenseTransactionsSum, SummaryRound);
        double total = Math.Round(incomes - expenses, SummaryRound);

        return new TransactionsSummaryDto(expenses, incomes, total);
    }

    public async Task<IEnumerable<TransactionsSumGroupByCategory>> GetTransactionsSumGroupByCategory(string userId, GetTransactionsRequest dto)
    {
        var transactions = await _unitOfWork.Transactions.GetFiltered(userId, dto.From, dto.To);

        var categoryIds = transactions.Select(transaction => transaction.CategoryId);

        var categories = await _unitOfWork.Categories.GetFiltered(categoryIds, dto.CategoryType);

        var filteredcategoryIds = categories.Select(category => category.Id);

        var transactionsGroupByCategoryId = transactions
            .Where(transaction => filteredcategoryIds.Contains(transaction.CategoryId))
            .GroupBy(transaction => transaction.CategoryId);


        var responseDto = new List<TransactionsSumGroupByCategory>();

        foreach (var transactionGroup in transactionsGroupByCategoryId)
        {
            var category = categories.First(category => category.Id == transactionGroup.Key);

            var transactionsSumByGroup = transactionGroup.Sum(transaction => transaction.Value);

            var transactionSumByCategory =
                new TransactionsSumGroupByCategory(
                    category.Id,
                    category.Type.ToString(),
                    category.Name,
                    category.PictureUrl,
                    category.IsDefault,
                    transactionsSumByGroup);

            responseDto.Add(transactionSumByCategory);
        }

        return responseDto;
    }

    public async Task<TransactionDto> Add(string userId, AddTransactionRequest dto)
    {
        var validationResult = await _addTransactionValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            if (validationResult.Errors.Any())
            {
                throw new AppIGetMoneyInvalidTransactionException(validationResult.Errors[0].ErrorMessage);
            }

            throw new AppIGetMoneyInvalidTransactionException();
        }

        var transaction = _mapper.Map<Transaction>(dto);

        var category = await _unitOfWork.Categories.GetCategoryWithUsersByIdAsync(dto.CategoryId);

        if (category == null) throw new AppIGetMoneyException();

        var user = category.Users.FirstOrDefault(user => user.Id == userId);

        if (user == null) throw new AppIGetMoneyException();

        transaction.UserId = userId;

        var transactionAdded = await _unitOfWork.Transactions.AddAsync(transaction);

        _unitOfWork.Complete();

        var responseDto = _mapper.Map<TransactionDto>(transactionAdded);

        return responseDto;
    }

    public async Task<TransactionDto> Edit(string userId, EditTransactionRequest dto)
    {
        var validationResult = await _editTransactionValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            if (validationResult.Errors.Any())
            {
                throw new AppIGetMoneyInvalidTransactionException(validationResult.Errors[0].ErrorMessage);
            }

            throw new AppIGetMoneyInvalidTransactionException();
        }

        var transaction = await _unitOfWork.Transactions.GetAsync(dto.Id);

        if (transaction == null) throw new AppIGetMoneyException();
        if (transaction.UserId != userId) throw new AppIGetMoneyException();

        transaction.Description = dto.Description;
        transaction.Value = dto.Value;
        transaction.Date = dto.Date;
        transaction.CategoryId = dto.CategoryId;

        var transactionUpdated = await _unitOfWork.Transactions.UpdateAsync(transaction);

        _unitOfWork.Complete();

        var responseDto = _mapper.Map<TransactionDto>(transactionUpdated);

        return responseDto;
    }

    public async Task<TransactionDto> Delete(string userId, int transactionId)
    {
        var transaction = await _unitOfWork.Transactions.GetAsync(transactionId);

        if (transaction == null) throw new AppIGetMoneyException();
        if (transaction.UserId != userId) throw new AppIGetMoneyException();

        var transactionDeleted = _unitOfWork.Transactions.HardDelete(transaction);

        _unitOfWork.Complete();

        var responseDto = _mapper.Map<TransactionDto>(transactionDeleted);

        return responseDto;
    }

    private double GetTransactionsSumByCategoryType(IEnumerable<Transaction> transactions, IEnumerable<Category> categories, CategoryType categoryType)
    {
        double sum = 0;

        if (transactions.Any() && categories.Any())
        {
            var categoriesIds = categories.Where(category => category.Type == categoryType).Select(category => category.Id);

            sum = transactions.Where(transaction => categoriesIds.Contains(transaction.CategoryId)).Sum(transaction => transaction.Value);
        }

        return sum;
    }
}
