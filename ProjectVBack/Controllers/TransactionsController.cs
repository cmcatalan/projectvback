using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Services;
using ProjectVBack.Crosscutting.CustomExceptions;
using System.Security.Claims;

namespace ProjectVBack.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionAppService _transactionsAppService;

        public TransactionsController(ILogger<TransactionsController> logger, ITransactionAppService transactionsAppService, IMapper mapper)
        {
            _logger = logger;
            _transactionsAppService = transactionsAppService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetTransactionsRequest request)
        {
            var userId = GetUserId();
            var transactions = await _transactionsAppService.GetAllTransactionsWithCategoryInfo(userId, request);

            return Ok(transactions);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetSummary([FromQuery] GetTransactionsSummaryRequest request)
        {
            var userId = GetUserId();
            var summary = await _transactionsAppService.GetSummary(userId, request);

            return Ok(summary);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTransactionsSumGroupByCategory([FromQuery] GetTransactionsRequest request)
        {
            var userId = GetUserId();
            var transactionsSumGroupByCategories = await _transactionsAppService.GetTransactionsSumGroupByCategory(userId, request);

            return Ok(transactionsSumGroupByCategories);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddTransactionRequest request)
        {
            var userId = GetUserId();
            var addedTransaction = await _transactionsAppService.Add(userId, request);

            return Ok(addedTransaction);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Edit(EditTransactionRequest request)
        {
            var userId = GetUserId();
            var editedTransaction = await _transactionsAppService.Edit(userId, request);

            return Ok(editedTransaction);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int transactionId)
        {
            var userId = GetUserId();
            var deletedTransaction = await _transactionsAppService.Delete(userId, transactionId);

            return Ok(deletedTransaction);
        }

        private string GetUserId()
        {
            var claims = HttpContext.User.Claims;

            if (claims == null || !claims.Any())
                throw new AppIGetMoneyException(nameof(claims));

            var claim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);

            if (claim == null || string.IsNullOrEmpty(claim.Value))
                throw new AppIGetMoneyException(nameof(claim));

            var userId = claim.Value;

            return userId;
        }
    }
}