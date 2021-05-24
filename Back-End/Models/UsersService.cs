using System;

namespace Back_End.Models
{
    public class UsersService : IUsersService
    {
        string USERS_INDEX = "users";
        private IElastic elastic;

        public UsersService(IElastic elastic)
        {
            this.elastic = elastic;            
            // check and create index if not created.
        }

    }
}