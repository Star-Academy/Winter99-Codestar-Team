using System;
using System.Collections.Generic;
using Back_End.Users;

namespace Back_End.Bank
{
    public interface IBankService
    {
        public Account GetAccount(string accountId) ;
        public List<Transaction> GetDestTransactions(string accountId) ;
        public List<Transaction> GetSrcTransactions(string accountId) ; 
        public List<Account> GetDestAccounts(string accountId) ;
        public List<Account> GetSrcAccounts(string accountId) ;
        public bool InsertAccount(Account account);
        public bool DeleteAccount(Account account);
        public bool UpdateAccount(Account account);
        public bool AccountExists(string field, string value);
        
        public Transaction GetTransaction(string transactionId) ;
        public Tuple<Account,Account> GetAccounts(string transactionId) ; /*returns src,dest */
        public bool InsertTransaction(Transaction transaction);
        public bool TransactionExists(string field, string value);

    }
}