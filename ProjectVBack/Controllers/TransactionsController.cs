using Microsoft.AspNetCore.Mvc;

namespace ProjectVBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ILogger<TransactionsController> logger)
        {
            _logger = logger;
        }
        // CRUD CREATE READ UPDATE DELETE --> CREATE = POST, READ = GET, UPDATE = PUT, DELETE = DELETE
        //TODO Get all transactions by user logged, from DateTime and to DateTime optional (fromDatetime, toDatetime = null)
        //TODO Get all transactions by type by user logged, from DateTime and to DateTime optional (type, fromDatetime, toDatetime = null)
        //TODO Get summary transactions by type by user logged, from DateTime and to DateTime optional (fromDatetime, toDatetime = null)
        // returns object with Summary(Incomes, Expenses, Total)
        //TODO Get transactions sum amount group by category by user logged, from DateTime and to DateTime optional 
        //TODO Post transaction by user logged, (description, categoryId, dateTimeTransaction, amount)
        //TODO Put transaction by user logged, (description, categoryId, dateTimeTransaction, amount)
        //TODO Delete transaction by user logged (id)
    }
}