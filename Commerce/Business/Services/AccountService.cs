using Commerce.Exceptions;
using Commerce.Global;
using Interview.DataAccess;
using System;
using System.Linq;

namespace Commerce.Business.Services
{
    public class AccountService : IAccountService
    {
        public string GetBalance(int userId, int accountId)
        {
            var user = DbContext.Users.FirstOrDefault(it => it.Id == userId);
            if (user == null)
            {
                throw new NotFoundException();
            }

            var acc = DbContext.Accounts.FirstOrDefault(it => it.User.Id == userId && it.Id == accountId);
            if (acc == null)
            {
                throw new NotFoundException();
            }

            return $"{Math.Round((double)acc.Balance, 2)} {acc.Currency}";
        }

        public void TopUpAccount(int userId, int accountId, double value)
        {
            var user = DbContext.Users.FirstOrDefault(it => it.Id == userId);
            if (user == null)
            {
                throw new NotFoundException();
            }

            var acc = DbContext.Accounts.FirstOrDefault(it => it.User.Id == userId && it.Id == accountId);
            if (acc == null)
            {
                throw new NotFoundException();
            }

            acc.Balance += (decimal)value;
        }

        public void ReduceAccount(int userId, int accountId, double value)
        {
            var user = DbContext.Users.FirstOrDefault(it => it.Id == userId);
            if (user == null)
            {
                throw new NotFoundException();
            }

            var acc = DbContext.Accounts.FirstOrDefault(it => it.User.Id == userId && it.Id == accountId);
            if (acc == null)
            {
                throw new NotFoundException();
            }

            if ((acc.Balance - (decimal)value) < 0)
            {
                throw new ValidationException(acc.Currency, userId, (double)acc.Balance);
            }

            acc.OperationsCount++;
            acc.Balance -= (decimal)value;

            user.BonusAccount.Balance += GetBonus(userId, accountId, value);
        }

        public decimal GetBonus(int userId, int accountId, double value)
        {
            var user = DbContext.Users.FirstOrDefault(it => it.Id == userId);
            if (user == null)
            {
                throw new NotFoundException();
            }

            var acc = DbContext.Accounts.FirstOrDefault(it => it.User.Id == userId && it.Id == accountId);
            if (acc == null)
            {
                throw new NotFoundException();
            }

            IBonusService bonus = new BonusService();
            var bonusValue = 0.0;
            var coefficient = GetCoefficientToUSD(acc.Currency);

            if (value > 5.0)
            {
                IBonusService bonusBySpentValue = new BonusBySpentValueDecorator(bonus);
                bonusValue = (double)bonusBySpentValue.GetBonus((decimal)value) * coefficient;

                if (acc.OperationsCount > 10)
                {
                    IBonusService bonusByOperationsCount = new BonusByOperationsCountDecorator(bonusBySpentValue);
                    bonusValue = (double)bonusByOperationsCount.GetBonus((decimal)value) * coefficient;
                }
            }

            return (decimal)bonusValue;
        }

        public double GetCoefficientToUSD(Currency currency)
        {
            var coefficient = 0.0;

            switch (currency)
            {
                case Currency.USD: coefficient = 1.00; break;
                case Currency.EUR: coefficient = 1.15; break;
                default: break;
            }

            return coefficient;
        }
    }
}
