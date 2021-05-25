using System.Globalization;
using Back_End.Elastic;
using Nest;
using Back_End.Models;
using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Back_End.Users
{
    public class UsersService : IUsersService
    {
        readonly string USERS_INDEX = "users"; // todo : add this to appsettings.json
        private IElastic elastic;

        public UsersService(IElastic elastic)
        {
            this.elastic = elastic;
            if (!elastic.IndexExists(USERS_INDEX))
            {
                elastic.CreateIndex<User>(USERS_INDEX, mapSelector: CreateMapping).Validate();
            }
        }

        public ITypeMapping CreateMapping(TypeMappingDescriptor<User> mappingDescriptor)
        {
            return mappingDescriptor.Properties(d => d
                                                .Keyword(k => k
                                                    .Name(u => u.UserId))
                                                .Keyword(k => k
                                                    .Name(u => u.Email))
                                                .Keyword(k => k
                                                    .Name(u => u.Salt))
                                                .Number(n => n.
                                                    Name(u => u.Hashed))
                                                );
        }

        public string Createhash(string pass, string salt){
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
    }
}