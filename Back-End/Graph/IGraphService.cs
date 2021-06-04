using System.Collections.Generic;
using Back_End.Bank;

namespace Back_End.Graph
{
    public interface IGraphService
    {

        public Dictionary<Account, Dictionary<Account, Transaction>> CreateGraph(Account src, Account dest, int maxLenght = 7);

        public int GetMaxFlow(Account s, Account t);

    }
}