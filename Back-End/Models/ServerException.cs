using System;

namespace SearchApi.Models
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message)
        { }
    }
}