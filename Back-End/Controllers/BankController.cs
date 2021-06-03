using System.Collections.Generic;
using System.Linq;
using Back_End.Bank;
using Back_End.Preprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;
        private readonly ICsvPreprocessor _csvPreprocessor;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
            _csvPreprocessor  = new TinyCsvPreprocessor();;
        }

        [HttpGet("{accountId}", Name = nameof(GetAccount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SummaryAccount> GetAccount(string accountId)
        {
           
            var account = _bankService.GetAccount(accountId);
            if (account is null)
                return NotFound();
            return Ok(SummaryAccount.Convert(account));
        }
        
        [HttpGet( Name = nameof(SearchAccount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<SummaryAccount>> SearchAccount([FromQuery]string query)
        {
            var accounts = _bankService.SearchAccount(query);
            if (accounts is null || !accounts.Any())
                return NotFound();
            return Ok(SummaryAccount.Convert(accounts));
        }
        
        [HttpGet("{transactionId}", Name = nameof(GetTransaction))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Transaction> GetTransaction(string transactionId)
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
            //use class nodeNeighbors
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
        public ActionResult AddUsers([FromBody] string csv)
        {
            var accounts = _csvPreprocessor.ParseAccounts(csv);
            if (accounts is null || !accounts.Any())
            {
                return BadRequest("Bad request to bulk accounts.");
            }

            _bankService.InsertAccounts(accounts);
            return Created("", "Accounts Inserted");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult AddTransactions([FromBody] string csv)
        {
            var transactions = _csvPreprocessor.ParseTransactions(csv);
            if (transactions is null || !transactions.Any())
            {
                return BadRequest("Bad request to bulk transactions.");
            }

            _bankService.InsertTransactions(transactions);
            return Created("", "Transactions Inserted");
        }
    }
}