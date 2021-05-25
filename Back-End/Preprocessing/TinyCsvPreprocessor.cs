using System.Collections.Generic;
using System.Linq;
using Back_End.Models;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace Back_End.Preprocessing
{
    public class TinyCsvPreprocessor : ICsvPreprocessor
    {
        private readonly string _newLine;
        private readonly char _separator;

        public TinyCsvPreprocessor(string newLine = "\n", char separator = ',')
        {
            _newLine = newLine;
            _separator = separator;
        }

        public IEnumerable<Account> ParseAccounts(string csvText)
        {
            return Parse(csvText, new CsvAccountMapping());
        }

        public IEnumerable<Transaction> ParseTransactions(string csvText)
        {
            return Parse(csvText, new CsvTransactionMapping());
        }

        private IEnumerable<T> Parse<T>(string csvText, ICsvMapping<T> mapping) where T : class, new()
        {
            return Parse(csvText,
                mapping,
                new CsvParserOptions(true, _separator),
                new CsvReaderOptions(new[] {_newLine}));
        }

        private static IEnumerable<T> Parse<T>(string csvText,
            ICsvMapping<T> mapping,
            CsvParserOptions parserOptions,
            CsvReaderOptions readerOptions) where T : class, new()
        {
            return new CsvParser<T>(parserOptions, mapping)
                .ReadFromString(readerOptions, csvText)
                .AsEnumerable()
                .Where(result => result.IsValid)
                .Select(result => result.Result);
        }
    }
}