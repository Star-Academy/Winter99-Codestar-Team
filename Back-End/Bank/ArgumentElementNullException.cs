using System;

namespace Back_End.Bank
{
    public class ArgumentElementNullException : Exception
    {
        public ArgumentElementNullException(string propertyName)
            : base($"{propertyName} of argument is null")
        {
        }
    }
}