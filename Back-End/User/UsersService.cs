using Back_End.Elastic;

namespace Back_End.User
{
    public class UsersService : IUsersService
    {
        readonly string USERS_INDEX = "users";
        private IElastic elastic;

        public UsersService(IElastic elastic)
        {
            this.elastic = elastic;            
            // check and create index if not created.
        }

    }
}