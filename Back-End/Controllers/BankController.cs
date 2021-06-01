using System;
using Back_End.Bank;
using Back_End.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BankController : ControllerBase
    {
        IBankService _bankService;

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
            //todo get bfs graph and maxflow
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
            if (account?.Id is null)
                return BadRequest(new ArgumentNullException(nameof(account.Id)));
            else if (account.Sheba is null)
                return BadRequest(new ArgumentNullException(nameof(account.Sheba)));
            else if (account.CardId is null)
                return BadRequest(new ArgumentNullException(nameof(account.CardId)));
            else if (account.Type is null)
                return BadRequest(new ArgumentNullException(nameof(account.Type)));
            else if (account.BranchAddress is null)
                return BadRequest(new ArgumentNullException(nameof(account.BranchAddress)));
            else if (account.BranchName is null)
                return BadRequest(new ArgumentNullException(nameof(account.BranchName)));
            else if (account.BranchTelephone is null)
                return BadRequest(new ArgumentNullException(nameof(account.BranchTelephone)));
            else if (account.OwnerId is null)
                return BadRequest(new ArgumentNullException(nameof(account.OwnerId)));
            else if (account.OwnerFamily is null)
                return BadRequest(new ArgumentNullException(nameof(account.OwnerFamily)));
            else if (account.OwnerName is null)
                return BadRequest(new ArgumentNullException(nameof(account.OwnerName)));

            if (_bankService.AccountExists(nameof(account.Id), account.Id))
                return Conflict($"A transaction with {account.Id} {nameof(account.Id)} already exists.");
            if (_bankService.AccountExists(nameof(account.CardId), account.CardId))
                return Conflict($"A transaction with {account.CardId} {nameof(account.CardId)} already exists.");
            if (_bankService.AccountExists(nameof(account.Sheba), account.Sheba))
                return Conflict($"A transaction with {account.Sheba} {nameof(account.Sheba)} already exists.");

            _bankService.InsertAccount(account);
            return CreatedAtAction(nameof(GetAccount), new {accountId = account.Id}, account);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult AddTransaction([FromBody] Transaction transaction)
        {
            if (transaction?.Id is null)
                return BadRequest(new ArgumentNullException(nameof(transaction.Id)));
            if (transaction.SrcAccountId is null)
                return BadRequest(new ArgumentNullException(nameof(transaction.SrcAccountId)));
            else if (transaction.DestAccountId is null)
                return BadRequest(new ArgumentNullException(nameof(transaction.DestAccountId)));
            else if (transaction.Type is null)
                return BadRequest(new ArgumentNullException(nameof(transaction.Type)));
            else if (transaction.TrackingId is null)
                return BadRequest(new ArgumentNullException(nameof(transaction.TrackingId)));
            else if (transaction.Amount == 0)
                return BadRequest(new ArgumentNullException(nameof(transaction.Amount)));
            else if (transaction.Date is null)
                return BadRequest(new ArgumentNullException(nameof(transaction.Date)));
            else if (transaction.Time is null)
                return BadRequest(new ArgumentNullException(nameof(transaction.Time)));

            if (_bankService.TransactionExists(nameof(transaction.Id), transaction.Id))
                return Conflict($"A transaction with {transaction.Id} {nameof(transaction.Id)} already exists.");
            if (_bankService.TransactionExists(nameof(transaction.TrackingId), transaction.TrackingId))
                return Conflict($"A transaction with {transaction.TrackingId} {nameof(transaction.TrackingId)} already exists.");

            _bankService.InsertTransaction(transaction);
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