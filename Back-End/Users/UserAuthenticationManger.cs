using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Back_End.Users
{
    public class UserAuthenticationManger : IUserAuthenticationManager
    {
        private const int SaltLength = 50;
        private const int SessionIdLength = 50;

        public string GenerateSessionId()
        {
            return RandomString(SessionIdLength);
        }

        public string GenerateSalt()
        {
            return RandomString(SaltLength);
        }

        public string GenerateHash(string pass, string salt)
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
    }
}