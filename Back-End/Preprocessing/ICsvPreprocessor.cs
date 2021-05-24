using System;
using System.Collections.Generic;
using Back_End.Models;

namespace Back_End.Preprocessing
{
    public interface ICsvPreprocessor
    {
        IEnumerable<Account> ParseAccounts(string csvText)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Account> ParseTransactions(string csvText)
        {
            throw new NotImplementedException();
        }
    }
}