using Back_End.Bank;
using TinyCsvParser.Mapping;

namespace Back_End.Preprocessing
{
    public class CsvAccountMapping : CsvMapping<Account>
    {
        public CsvAccountMapping()
        {
            MapProperty(0, account => account.Id);
            MapProperty(1, account => account.CardId);
            MapProperty(2, account => account.Sheba);
            MapProperty(3, account => account.Type);
            MapProperty(4, account => account.BranchTelephone);
            MapProperty(5, account => account.BranchAddress);
            MapProperty(6, account => account.BranchName);
            MapProperty(7, account => account.OwnerName);
            MapProperty(8, account => account.OwnerFamily);
            MapProperty(9, account => account.OwnerId);
        }
    }
}