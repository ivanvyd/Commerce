using Commerce.Global;
using System;
using System.Runtime.Serialization;

namespace Commerce.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        { }

        public ValidationException(Currency currency, int userId, double balance)
            : base($"{currency} account of this user (id = {userId}) has not enough money (balance: {balance}).")
        { }
    }
}
