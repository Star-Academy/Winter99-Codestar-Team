using System;
using System.Collections.Generic;
using System.Linq;
using Back_End.Elastic;
using Back_End.StaticServices;

namespace Back_End.Bank
{
    public class BankService : IBankService
    {
        private readonly Elastic<Account> _accountsElastic;
        private readonly Elastic<Transaction> _transactionsElastic;

        public BankService(Elastic<Account> accountsElastic, Elastic<Transaction> transactionsElastic)
        {
            _accountsElastic = accountsElastic;
            _transactionsElastic = transactionsElastic;
        }

        // ******************************************* accounts ******************************************* //

        public Account GetAccount(string accountId)
        {
            return _accountsElastic.GetDocument(accountId);
        }

        public List<Transaction> GetDestTransactions(string accountId)
        {
            return GetAccount(accountId)?
                .DestTransactions
                .Select(GetTransaction)
                .ToList();
        }

        public List<Account> GetDestAccounts(string accountId)
        {
            return GetAccount(accountId)?.DestTransactions
                .Select(transactionId => GetTransaction(transactionId).DestAccountId)
                .Select(GetAccount)
                .ToList();
        }

        public List<Transaction> GetSrcTransactions(string accountId)
        {
            return GetAccount(accountId)?
                .SrcTransactions
                .Select(GetTransaction)
                .ToList();
        }

        public List<Account> GetSrcAccounts(string accountId)
        {
            return GetAccount(accountId)?
                .SrcTransactions
                .Select(transactionId => GetTransaction(transactionId).DestAccountId)
                .Select(GetAccount)
                .ToList();
        }

        public bool InsertAccount(Account account)
        {
            var tempAccount = GetAccount(account.Id);
            if (tempAccount != null)
                return false;
            try
            {
                _accountsElastic.Index(account, x => x.Id).Validate();
            }
            catch (Exception e)
            {
                //todo log error
                return false;
            }

            return true;
        }

        public bool DeleteAccount(Account account)
        {
            Account tempAccount = GetAccount(account.Id);
            if (tempAccount != null)
                return false;
            try
            {
                _accountsElastic.Index(account, x => x.Id).Validate();
            }
            catch (Exception e)
            {
                //todo log error
                return false;
            }

            return true;
        }

        public bool UpdateAccount(Account account)
        {
            return DeleteAccount(account) && InsertAccount(account);
        }

        public bool AccountExists(string field, string value)
        {
            var response = _accountsElastic
                .GetResponseOfQuery(_accountsElastic.MakeTermQuery(value, StringStuffs.MakeCamelCase(field)))
                .Validate();
            return response.Hits.Any();
        }


        // ******************************************* transactions ******************************************* //
        public Transaction GetTransaction(string transactionId)
        {
            return _transactionsElastic.GetDocument(transactionId);
        }

        public Tuple<Account, Account> GetAccounts(string transactionId)
        {
            Transaction transaction = GetTransaction(transactionId);
            if (transaction == null)
                return new Tuple<Account, Account>(null, null);
            Account srcAccount = GetAccount(transaction.SrcAccountId);
            Account destAccount = GetAccount(transaction.DestAccountId);
            if (srcAccount == null || destAccount == null)
            {
                return new Tuple<Account, Account>(null, null);
            }
            else
            {
                return new Tuple<Account, Account>(srcAccount, destAccount);
            }
        }

        public bool InsertTransaction(Transaction transaction)
        {
            //todo may be it needs a better error handling...

            var srcAccount = GetAccount(transaction.SrcAccountId);
            var destAccount = GetAccount(transaction.DestAccountId);
            var tempTransaction = GetTransaction(transaction.Id);
            if (srcAccount == null || destAccount == null || tempTransaction != null)
                return false;
            srcAccount.SrcTransactions.Add(transaction.Id);
            destAccount.DestTransactions.Add(transaction.Id);
            try
            {
                var srcAccountResponse = _accountsElastic.Index(srcAccount, x => x.Id).Validate();
                var destAccountResponse = _accountsElastic.Index(destAccount, x => x.Id).Validate();
                var transactionResponse = _transactionsElastic.Index(transaction, x => x.Id).Validate();
                if (srcAccountResponse == null || destAccountResponse == null || transactionResponse == null)
                    return false;
            }
            catch (Exception e)
            {
                //todo log error
                return false;
            }

            return true;
        }

        public bool TransactionExists(string field, string value)
        {
            var response = _accountsElastic
                .GetResponseOfQuery(_accountsElastic.MakeTermQuery(value, StringStuffs.MakeCamelCase(field)))
                .Validate();
            return response.Hits.Any();
        }
    }
}