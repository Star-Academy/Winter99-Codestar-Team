namespace Back_End.Models
{
    public class BankService : IBankService
    {

        readonly string ACCOUNTS_INDEX = "accounts";
        readonly string TRANSACTIONS_INDEX = "transactions";
        private IElastic elastic;

        public BankService(IElastic elastic)
        {
            this.elastic = elastic;
            // check and create indices if not created.
        }
    }
}