using System.Linq;
using Back_End.Elastic;
using Back_End.StaticServices;

namespace Back_End.Users
{
    public class UsersService : IUsersService
    {
        private readonly Elastic<User> _usersElastic;
        private readonly IUserAuthenticationManager _authenticationManager;

        public UsersService(Elastic<User> usersElastic, IUserAuthenticationManager authenticationManager)
        {
            _usersElastic = usersElastic;
            _authenticationManager = authenticationManager;
        }

        public void AddUser(User user)
        {
            user.Salt = _authenticationManager.GenerateSalt();
            user.Hashed = _authenticationManager.GenerateHash(user.Password, user.Salt);
            _usersElastic.Index(user, userType => userType.UserId).Validate();
        }

        public User GetUser(string userId)
        {
            return _usersElastic.GetDocument(userId);
        }

        public bool Exists(string field, string value)
        {
            var response = _usersElastic.GetResponseOfQuery(_usersElastic.MakeTermQuery(value, StringStuffs.MakeCamelCase(field)))
                .Validate();
            return response.Hits.Any();
        }
        

        public bool CheckUser(string userId, string password)
        {
            var user = GetUser(userId);
            return user is not null && user.Hashed == _authenticationManager.GenerateHash(password, user.Salt);
        }

        public Session CreateSession()
        {
            return new(_authenticationManager.GenerateSessionId());
        }
    }
}