using System.Collections.Generic;

namespace Back_End.Bank
{
    public class NodeNeighbors
    {
        
        //todo make an appropriate constructor
        public BriefAccount Account { get; }
        public Dictionary<BriefAccount, List<BriefTransaction>> srcAccounts { get; }
        public Dictionary<BriefAccount, List<BriefTransaction>> destAccounts { get; }
    }
}