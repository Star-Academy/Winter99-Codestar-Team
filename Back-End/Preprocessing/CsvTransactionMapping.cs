using Back_End.Bank;
using TinyCsvParser.Mapping;

namespace Back_End.Preprocessing
{
    public class CsvTransactionMapping : CsvMapping<Transaction>
    {
        public CsvTransactionMapping()
        {
            MapProperty(0, transaction => transaction.SrcAccountId);
            MapProperty(1, transaction => transaction.DestAccountId);
            MapProperty(2, transaction => transaction.GetDate());
            MapProperty(3, transaction => transaction.GetTime());
            MapProperty(4, transaction => transaction.TrackingId);
            MapProperty(5, transaction => transaction.Amount);
            MapProperty(6, transaction => transaction.Type);
            MapProperty(7, transaction => transaction.Id);
        }
    }
}