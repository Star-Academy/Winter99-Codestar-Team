using System.Collections.Generic;
using Back_End.Bank;

namespace Back_End.Graph
{
    public class GraphService : IGraphService
    { 
        private readonly IBankService _bankService;

        private Dictionary<Account, Dictionary<Account, Transaction>> Graph;

        public GraphService(IBankService bankService)
        {
            _bankService = bankService;
        }


    }
}