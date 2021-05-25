using Back_End.Bank;
using Microsoft.Extensions.Configuration;
using Nest;

namespace Back_End.Elastic
{
    public class AccountsElastic : Elastic<Account>
    {
        public AccountsElastic(IConfiguration configuration) :
            base(configuration, configuration["accountsIndex"])
        {
        }

        protected override ITypeMapping CreateMapping(TypeMappingDescriptor<Account> mappingDescriptor)
        {
            throw new System.NotImplementedException();
        }
    }
}