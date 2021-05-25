using Back_End.Bank;
using Microsoft.Extensions.Configuration;
using Nest;

namespace Back_End.Elastic
{
    public class TransactionsElastic : Elastic<Transaction>
    {
        public TransactionsElastic(IConfiguration configuration) : base(configuration,
            configuration["transactionsIndex"])
        {
        }

        protected override ITypeMapping CreateMapping(TypeMappingDescriptor<Transaction> mappingDescriptor)
        {
            throw new System.NotImplementedException();
        }
    }
}