using System;

namespace Back_End.Elastic
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message)
        {
        }
    }
}