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

        public string CreateHash(string pass, string salt)
        {
            HashAlgorithm sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(salt + pass)));
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void AddUser(User user)
        {
            user.Salt = RandomString(50);
            user.Hashed = CreateHash(user.Password, user.Salt);
            _usersElastic.Index(user, user => user.UserId).Validate();
        }

        public User GetUser(string userId)
        {
            return _usersElastic.GetDocument(userId);
        }

        public bool Exists(string field, string value)
        {
            var response = _usersElastic.GetResponseOfQuery(_usersElastic.MakeTermQuery(value, MakeCamelCase(field))).Validate();
            if (response.Hits.Any())
                return true;
            return false;
        }

        public string MakeCamelCase(string text)
        {
            return Char.ToLowerInvariant(text[0]) + text.Substring(1);
        }

        public bool CheckUser(string userId, string password)
        {
            var user = GetUser(userId);
            if (user is null || user.Hashed != CreateHash(password, user.Salt))
                return false;
            return true;
        }

        public Session CreateSession()
        {
            return new Session(RandomString(50));
        }
    }
}