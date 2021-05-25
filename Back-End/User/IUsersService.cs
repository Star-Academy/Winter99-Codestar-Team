using Back_End.Models;
using Nest;

namespace Back_End.Users
{
    public interface IUsersService
    {
        public ITypeMapping CreateMapping(TypeMappingDescriptor<User> mappingDescriptor);

    }
}