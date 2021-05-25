using Back_End.Models;
using TinyCsvParser.Mapping;

namespace Back_End.Preprocessing
{
    public class CsvTransactionMapping : CsvMapping<Transaction>
    {
        public CsvTransactionMapping()
        {
            MapProperty(0, transaction => transaction.SourceAccountId);
            MapProperty(1, transaction => transaction.DestinationAccountId);
            MapProperty(2, transaction => transaction.Date);
            MapProperty(3, transaction => transaction.Time);
            MapProperty(4, transaction => transaction.TrackingId);
            MapProperty(5, transaction => transaction.Amount);
            MapProperty(6, transaction => transaction.Type);
            MapProperty(7, transaction => transaction.TransactionId);
        }
    }
}