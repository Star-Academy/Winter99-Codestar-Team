using System.Collections.Generic;

namespace Back_End.Bank
{
    public interface IBankService
    {
        public List<Transaction> GetDestTransactions(string accountId) ;
        public List<Transaction> GetSrcTransactions(string accountId) ;
        public Dictionary<string,Account> GetAccounts(string transactionId) ;

        public bool PushAccount(Account account);
        public bool PushTransaction(Transaction destTransaction , Transaction srcTransaction);
        

    }
}