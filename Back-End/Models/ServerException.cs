using System;

namespace Back_End.Models
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message)
        { }
    }
}