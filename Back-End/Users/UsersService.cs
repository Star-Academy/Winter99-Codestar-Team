using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Back_End.Elastic;

namespace Back_End.Users
{
    public class UsersService : IUsersService
    {
        private readonly Elastic<User> _usersElastic;

        public UsersService(Elastic<User> usersElastic)
        {
            _usersElastic = usersElastic;
        }

        private static string CreateHash(string pass, string salt)
        {
            var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(salt + pass)));
        }

        private static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void AddUser(User user)
        {
            user.Salt = RandomString(50);
            user.Hashed = CreateHash(user.Password, user.Salt);
            _usersElastic.Index(user, userType => userType.UserId).Validate();
        }

        public User GetUser(string userId)
        {
            return _usersElastic.GetDocument(userId);
        }

        public bool Exists(string field, string value)
        {
            var response = _usersElastic.GetResponseOfQuery(_usersElastic.MakeTermQuery(value, MakeCamelCase(field))).Validate();
            return response.Hits.Any();
        }

        private static string MakeCamelCase(string text)
        {
            return char.ToLowerInvariant(text[0]) + text[1..];
        }

        public bool CheckUser(string userId, string password)
        {
            var user = GetUser(userId);
            return user is not null && user.Hashed == CreateHash(password, user.Salt);
        }

        public Session CreateSession()
        {
            return new Session(RandomString(50));
        }
    }
}