using Commerce.Accounts;
using Commerce.Bonuses;
using Commerce.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Users
{
    public class User
    {
        public User()
        {
            Id = 1;
            Name = "John";
            Surname = "Travolta";
            BonusAccount = new Account(Id, 0, Currency.USD);
            Accounts = new List<Account>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public IList<Account> Accounts { get; set; }

        public Account BonusAccount { get; set; }

        public void GetUserInfo()
        {
            Console.WriteLine($"{Name} {Surname} (id {Id}):");
            GetAccounts();
            Console.WriteLine();
        }

        public void AddAccount(Currency currency)
        {
            if (Accounts.FirstOrDefault(it => it.Currency == currency) != null)
            {
                Console.WriteLine($"Error! This user has account with {currency} currency.");
                return;
            }

            Console.WriteLine($">> add {currency} account to the user\n");
            Accounts.Add(new Account(Id, 0, currency));
        }

        public void DeleteAccount(Currency currency)
        {
            var acc = Accounts.FirstOrDefault(it => it.Currency == currency);
            if (acc == null)
            {
                Console.WriteLine($"Error! This user hasn't account with {currency} currency.");
                return;
            }

            if (acc.Balance > 0)
            {
                Console.WriteLine($"Error! This user's account can't be deleted. Balance is not 0 ({acc.GetBalance()}).");
                return;
            }

            Console.WriteLine($">> delete {currency} account from the user\n");
            Accounts.Remove(acc);
        }

        public void GetAccounts()
        {
            Console.WriteLine($"Bonus account: {BonusAccount.Balance} {BonusAccount.Currency}");
            foreach (var account in Accounts)
            {
                Console.WriteLine($"Account #{account.Id}: {account.Balance} {account.Currency}");
            }
        }

        public void TopUpAccount(Currency currency, double value)
        {
            var acc = Accounts.FirstOrDefault(it => it.Currency == currency);
            if (acc == null)
            {
                Console.WriteLine($"Error! This user hasn't account with {currency} currency.");
                return;
            }

            Console.WriteLine($">> add {value} {currency} to the user's {currency} account\n");
            acc.Balance += value;
        }

        public void ReduceAccount(Currency currency, double value)
        {
            var acc = Accounts.FirstOrDefault(it => it.Currency == currency);
            if (acc == null)
            {
                Console.WriteLine($"Error! This user hasn't account with {currency} currency.");
                return;
            }

            if ((acc.Balance - value) < 0)
            {
                Console.WriteLine($"Error! Not enough money on the balance ({acc.GetBalance()}).");
                return;
            }

            acc.OperationsCount++;
            Console.WriteLine($">> deduct {value} {currency} from the user's {currency} account\n");
            acc.Balance -= value;
            GetBonus(value, acc);
        }

        private void GetBonus(double value, Account acc)
        {
            IBonus bonus = new Bonus();
            double bonusValue = 0;
            double coefficient = 0;

            switch (acc.Currency)
            {
                case Currency.USD: coefficient = 1; break;
                case Currency.EUR: coefficient = 1.15; break;
                default: break;
            }

            
            if (value > 5)
            {
                IBonus bonusBySpentValue = new BonusBySpentValueDecorator(bonus);
                bonusValue = bonusBySpentValue.GetBonus(value) * coefficient;
                Console.WriteLine($">> calculated bonus by spent value (> 5): {Math.Round(bonusValue, 2)}\n");

                if (acc.OperationsCount > 10)
                {
                    IBonus bonusByOperationsCount = new BonusByOperationsCountDecorator(bonusBySpentValue);
                    bonusValue = bonusByOperationsCount.GetBonus(value) * coefficient;
                    Console.WriteLine($">> calculated bonus by operations count (> 10): {Math.Round(bonusValue, 2)}\n");
                }
            }

            Console.WriteLine($">> add {Math.Round(bonusValue, 2)} USD to the user's bonus account\n");
            BonusAccount.Balance += Math.Round(bonusValue, 2);
        }
    }
}
