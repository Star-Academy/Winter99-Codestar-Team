
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
            //todo a new format for account without src and dest transactions
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
        
        [HttpGet("bfs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetGraph([FromBody] string srcAccountId,[FromBody] string destAccountId,[FromBody] int depth)
        {
            //todo get bfs graph and maxflow
            return null;
        }
        
        [HttpGet("neighbors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetNeighbors([FromBody] string parameters)
        {
            //todo get neighbors
            
            return null;
        }

    }
}