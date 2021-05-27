using Back_End.Elastic;

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
        
        
    }
}