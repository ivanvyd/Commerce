using Commerce.Data.Entities;
using Commerce.Global;
using System.Collections.Generic;

namespace Interview.DataAccess
{
    public class DbContext
    {
        private static readonly User User1 = new() { Id = 1, Name = "John", Surname = "Travolta" };

        private static readonly Account BonusAccount = new() { Id = 0, Currency = Currency.USD, Balance = 0.0m, OperationsCount = 0, User = User1 };
        private static readonly Account Account1 = new() { Id = 1, Currency = Currency.USD, Balance = 0.0m, OperationsCount = 0, User = User1 };
        private static readonly Account Account2 = new() { Id = 2, Currency = Currency.EUR, Balance = 0.0m, OperationsCount = 0, User = User1 };

        static DbContext()
        {
            User1.BonusAccount = BonusAccount;
            User1.Accounts = new List<Account> { Account1, Account2 };
        }

        public static User[] Users = new User[]
        {
            User1,
        };

        public static Account[] Accounts = new Account[]
        {
            BonusAccount,
            Account1,
            Account2
        };
    }
}
