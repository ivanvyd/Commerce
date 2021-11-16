using Commerce.Business;
using Commerce.Business.Services;
using Commerce.Global;
using Interview.DataAccess;
using System;
using System.Linq;

namespace Commerce
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserService userService = new UserService();
            IAccountService accountService = new AccountService();

            var user = DbContext.Users.FirstOrDefault();
            var bonusAccount = DbContext.Accounts.FirstOrDefault(it => it.Id == 0 && it.User.Id == user.Id);
            var accountEUR = DbContext.Accounts.FirstOrDefault(it => it.Currency == Currency.EUR && it.User.Id == user.Id);

            Console.WriteLine(">> Get user's info\n");
            Console.WriteLine(userService.GetUserInfo(user.Id));

            Console.WriteLine("\n>> Top up user's EUR account by 1000 EUR\n");
            accountService.TopUpAccount(user.Id, accountEUR.Id, 1000.0);

            Console.WriteLine(">> Get user's EUR account balance\n");
            Console.WriteLine(accountService.GetBalance(user.Id, accountEUR.Id));

            Console.WriteLine("\n>> Reduce user's EUR account by 10 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 10.0);

            var expectedBonusBalance = 0.0;
            var expectedBonus = Math.Round(10.0 * accountService.GetCoefficientToUSD(Currency.EUR) * 0.01, 3); // 1% bonus for spent value
            expectedBonusBalance += expectedBonus;
            Console.WriteLine($">> Expected bonus account balance = { expectedBonusBalance } USD (+{ expectedBonus } USD)\n");
            Console.WriteLine($">> Current bonus account balance = { bonusAccount.Balance } USD\n");
            Console.WriteLine(">> Get user's bonus account balance\n");
            Console.WriteLine(accountService.GetBalance(user.Id, bonusAccount.Id));

            Console.WriteLine("\n>> Reduce user's EUR account by 1 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 1.0);
            Console.WriteLine(">> Reduce user's EUR account by 1 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 1.0);
            Console.WriteLine(">> Reduce user's EUR account by 1 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 1.0);
            Console.WriteLine(">> Reduce user's EUR account by 1 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 1.0);
            Console.WriteLine(">> Reduce user's EUR account by 1 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 1.0);
            Console.WriteLine(">> Reduce user's EUR account by 1 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 1.0);
            Console.WriteLine(">> Reduce user's EUR account by 1 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 1.0);
            Console.WriteLine(">> Reduce user's EUR account by 1 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 1.0);

            Console.WriteLine(">> Reduce user's EUR account by 10 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 10.0);

            expectedBonus = Math.Round(10.0 * accountService.GetCoefficientToUSD(Currency.EUR) * 0.01, 3); // 1% bonus for spent value
            expectedBonusBalance += expectedBonus;
            Console.WriteLine($">> Expected bonus account balance = { expectedBonusBalance } USD (+{ expectedBonus } USD)\n");
            Console.WriteLine($">> Current bonus account balance = { bonusAccount.Balance } USD\n");
            Console.WriteLine(">> Get user's bonus account balance\n");
            Console.WriteLine(accountService.GetBalance(user.Id, bonusAccount.Id));

            Console.WriteLine("\n>> Reduce user's EUR account by 10 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 10.0);

            expectedBonus = Math.Round(10.0 * accountService.GetCoefficientToUSD(Currency.EUR) * 0.01, 3); // 1% bonus for spent value
            expectedBonus += Math.Round(10.0 * accountService.GetCoefficientToUSD(Currency.EUR) * 0.01, 3); // additional 1% bonus for operations count
            expectedBonusBalance += expectedBonus;
            Console.WriteLine($">> Expected bonus account balance = { expectedBonusBalance } USD (+{ expectedBonus } USD)\n");
            Console.WriteLine($">> Current bonus account balance = { bonusAccount.Balance } USD\n");
            Console.WriteLine(">> Get user's bonus account balance\n");
            Console.WriteLine(accountService.GetBalance(user.Id, bonusAccount.Id));

            Console.WriteLine("\n>> Reduce user's EUR account by 13.24 EUR\n");
            accountService.ReduceAccount(user.Id, accountEUR.Id, 13.24);

            expectedBonus = Math.Round(13.24 * accountService.GetCoefficientToUSD(Currency.EUR) * 0.01, 2); // 1% bonus for spent value
            expectedBonus += Math.Round(13.24 * accountService.GetCoefficientToUSD(Currency.EUR) * 0.01, 2); // additional 1% bonus for operations count
            expectedBonusBalance += expectedBonus;
            Console.WriteLine($">> Expected bonus account balance = { expectedBonusBalance } USD (+{ expectedBonus } USD)\n");
            Console.WriteLine($">> Current bonus account balance = { bonusAccount.Balance } USD\n");
            Console.WriteLine(">> Get user's bonus account balance\n");
            Console.WriteLine(accountService.GetBalance(user.Id, bonusAccount.Id));

            Console.ReadKey();
        }
    }
}
