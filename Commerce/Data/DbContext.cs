using Commerce.Data.Entities;
using Commerce.Global;
using System.Collections.Generic;

namespace Interview.DataAccess
{
    public class DbContext
    {
        public User[] Users;

        public Account[] Accounts;

        public DbContext()
        {
            Seed();
        }

        private void Seed()
        {
            var User1 = new User() { Id = 1, Name = "John", Surname = "Travolta" };

            var BonusAccount = new Account() { Id = 0, Currency = Currency.USD, Balance = 0.0m, OperationsCount = 0, User = User1 };
            var Account1 = new Account() { Id = 1, Currency = Currency.USD, Balance = 0.0m, OperationsCount = 0, User = User1 };
            var Account2 = new Account() { Id = 2, Currency = Currency.EUR, Balance = 0.0m, OperationsCount = 0, User = User1 };

            User1.BonusAccount = BonusAccount;
            User1.Accounts = new List<Account> { Account1, Account2 };

            Users = new User[]
            {
                User1,
            };

            Accounts = new Account[]
            {
                BonusAccount,
                Account1,
                Account2
            };
        }
    }
}
