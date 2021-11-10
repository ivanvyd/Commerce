using Commerce.Global;
using Commerce.Users;
using System;

namespace Commerce
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User();
            //user.GetUserInfo();

            user.AddAccount(Currency.USD);
            //user.GetUserInfo();

            user.AddAccount(Currency.EUR);
            user.GetUserInfo();

            user.TopUpAccount(Currency.EUR, 1000);

            user.ReduceAccount(Currency.EUR, 50);
            user.ReduceAccount(Currency.EUR, 2);
            user.ReduceAccount(Currency.EUR, 2);
            user.ReduceAccount(Currency.EUR, 7);
            user.ReduceAccount(Currency.EUR, 2);
            user.ReduceAccount(Currency.EUR, 2);
            user.ReduceAccount(Currency.EUR, 1);
            user.ReduceAccount(Currency.EUR, 2);
            user.ReduceAccount(Currency.EUR, 2);
            user.ReduceAccount(Currency.EUR, 6);
            user.ReduceAccount(Currency.EUR, 6);

            user.GetUserInfo();

            Console.ReadKey();
        }
    }
}
