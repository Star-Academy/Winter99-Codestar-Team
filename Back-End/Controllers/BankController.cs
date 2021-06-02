using Back_End.Bank;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet("{accountId}", Name = nameof(GetAccount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Account> GetAccount(string accountId)
        {
            //todo a new format for transaction without src and dest transactions
            var account = _bankService.GetAccount(accountId);
            if (account is null)
                return NotFound();
            return Ok(account);
        }

        [HttpGet("{transactionId}", Name = nameof(GetTransaction))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Account> GetTransaction(string transactionId)
        {
            var transaction = _bankService.GetTransaction(transactionId);
            if (transaction is null)
                return NotFound();
            return Ok(transaction);
        }

        [HttpGet(Name = nameof(GetGraph))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetGraph([FromQuery] string srcAccountId, [FromQuery] string destAccountId,
            [FromQuery] int depth)
        {
            //todo get bfs graph and max flow
            return null;
        }

        [HttpGet(Name = nameof(GetNeighbors))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetNeighbors([FromQuery] string parameters)
            //todo edit parameters
        {
            //todo get neighbors

            return null;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult AddUser([FromBody] Account account)
        {
            if (account is null)
            {
                return BadRequest("Argument is null");
            }

            try
            {
                account.ValidateProperties();
            }
            catch (ArgumentElementNullException e)
            {
                return BadRequest(e.Message);
            }


            if (_bankService.AccountExists(nameof(account.Id), account.Id))
                return Conflict($"A transaction with {account.Id} {nameof(account.Id)} already exists.");
            if (_bankService.AccountExists(nameof(account.CardId), account.CardId))
                return Conflict($"A transaction with {account.CardId} {nameof(account.CardId)} already exists.");
            if (_bankService.AccountExists(nameof(account.Sheba), account.Sheba))
                return Conflict($"A transaction with {account.Sheba} {nameof(account.Sheba)} already exists.");

            _bankService.InsertAccount(account); // todo : return result of this action to the client
            return CreatedAtAction(nameof(GetAccount), new {accountId = account.Id}, account);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult AddTransaction([FromBody] Transaction transaction)
        {
            if (transaction is null)
            {
                return BadRequest("Argument is null");
            }

            try
            {
                transaction.ValidateProperties();
            }
            catch (ArgumentElementNullException e)
            {
                return BadRequest(e.Message);
            }

            if (_bankService.TransactionExists(nameof(transaction.Id), transaction.Id))
                return Conflict($"A transaction with {transaction.Id} {nameof(transaction.Id)} already exists.");
            if (_bankService.TransactionExists(nameof(transaction.TrackingId), transaction.TrackingId))
                return Conflict(
                    $"A transaction with {transaction.TrackingId} {nameof(transaction.TrackingId)} already exists.");

            _bankService.InsertTransaction(transaction); // todo : return result of this action to the client 
            return CreatedAtAction(nameof(GetTransaction), new {transactionId = transaction.Id}, transaction);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult AddUsers()
        {
            return null;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult AddTransactions()
        {
            return null;
        }
    }
}