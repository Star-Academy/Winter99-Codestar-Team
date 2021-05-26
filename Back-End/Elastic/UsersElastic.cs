using Back_End.Users;
using Microsoft.Extensions.Configuration;
using Nest;

namespace Back_End.Elastic
{
    public class UsersElastic : Elastic<User>
    {
        public UsersElastic(IConfiguration configuration) : base(configuration, configuration["usersIndex"])
        {
        }

        protected override ITypeMapping CreateMapping(TypeMappingDescriptor<User> mappingDescriptor)
        {
            return mappingDescriptor.Properties(d => d
                .Keyword(k => k
                    .Name(u => u.UserId))
                .Keyword(k => k
                    .Name(u => u.Email))
                .Keyword(k => k
                    .Name(u => u.Salt))
                .Keyword(k => k
                    .Name(u => u.Hashed))
            );
        }
    }
}