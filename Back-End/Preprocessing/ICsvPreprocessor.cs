using System.Collections.Generic;
using Back_End.Bank;

namespace Back_End.Preprocessing
{
    public interface ICsvPreprocessor
    {
        IEnumerable<Account> ParseAccounts(string csvText);

        IEnumerable<Transaction> ParseTransactions(string csvText);
    }
}