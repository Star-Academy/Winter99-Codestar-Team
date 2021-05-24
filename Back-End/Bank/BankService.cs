using Back_End.Elastic;

namespace Back_End.Bank
{
    public class BankService : IBankService
    {
        readonly string ACCOUNTS_INDEX = "accounts"; // todo : add this to appsettings.json
        readonly string TRANSACTIONS_INDEX = "transactions"; // todo : add this to appsettings.json
        private IElastic elastic;

        public BankService(IElastic elastic)
        {
            this.elastic = elastic;
            // check and create indices if not created.
        }
    }
}