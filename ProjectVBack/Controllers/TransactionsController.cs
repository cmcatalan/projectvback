using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectVBack.Application.Dtos;
using System.Security.Claims;

namespace ProjectVBack.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ILogger<TransactionsController> logger)
        {
            _logger = logger;
        }
        // CRUD CREATE READ UPDATE DELETE --> CREATE = POST, READ = GET, UPDATE = PUT, DELETE = DELETE
        //TODO Get all transactions by user logged, from DateTime and to DateTime optional (fromDatetime, toDatetime = null)
        //TODO Get summary transactions by type by user logged, from DateTime and to DateTime optional (fromDatetime, toDatetime = null)
        // returns object with Summary(Incomes, Expenses, Total)
        //TODO Get transactions sum amount group by category by user logged, from DateTime and to DateTime optional 
        //TODO Post transaction by user logged, (description, categoryId, dateTimeTransaction, amount)
        //TODO Put transaction by user logged, (description, categoryId, dateTimeTransaction, amount)
        //TODO Delete transaction by user logged (id)

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetTransactionsRequest request)
        {
            var userId = GetUserId();

            var transactions = new List<TransactionCategoryDto>();
            transactions.Add(new TransactionCategoryDto(1, "description transaction 1", 12.5, DateTime.Now.AddDays(-1), "Expense", "Cinema", "", true));
            transactions.Add(new TransactionCategoryDto(2, "description transaction 2", 14.5, DateTime.Now.AddDays(-2), "Expense", "Restaurant", "", true));
            transactions.Add(new TransactionCategoryDto(3, "description transaction 3", 2.5, DateTime.Now.AddDays(-2), "Expense", "Pen", "", true));
            transactions.Add(new TransactionCategoryDto(4, "description transaction 4", 100, DateTime.Now.AddDays(-3), "Expense", "Travel", "", true));
            //todo call transactions service
            return Ok(transactions);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetSummary([FromQuery] GetTransactionsRequest request)
        {
            var userId = GetUserId();
            var summary = new TransactionsSummaryDto(90, 100, 90 - 100);
            //todo call transactions service
            return Ok(summary);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTransactionsSumGroupByCategory([FromQuery] GetTransactionsRequest request)
        {
            var userId = GetUserId();

            var transactionsSumGroupByCategories = new List<TransactionsSumGroupByCategory>();

            transactionsSumGroupByCategories.Add(new TransactionsSumGroupByCategory(1, "Expense", "Cinema", "", true, 100.2));
            transactionsSumGroupByCategories.Add(new TransactionsSumGroupByCategory(2, "Expense", "Restaurant", "", true, 99.2));
            //todo call transactions service
            return Ok(transactionsSumGroupByCategories);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddTransactionRequest request)
        {
            var userId = GetUserId();


            //todo call transactions service
            return Ok(new TransactionDto(1, "", 10.2, DateTime.Now, 1));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Edit(EditTransactionRequest request)
        {
            var userId = GetUserId();

            //todo call transactions service
            return Ok(new TransactionDto(1, "", 10.2, DateTime.Now, 1));
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            //todo call transactions service delete transaction
            return Ok(new TransactionDto(1, "", 10.2, DateTime.Now, 1));
        }

        private string GetUserId()
        {
            var claims = HttpContext.User.Claims;

            if (claims == null || !claims.Any())
                throw new ArgumentNullException(nameof(claims));

            var claim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);

            if (claim == null || string.IsNullOrEmpty(claim.Value))
                throw new NullReferenceException(nameof(claim));

            var userId = claim.Value;

            return userId;
        }
    }
}