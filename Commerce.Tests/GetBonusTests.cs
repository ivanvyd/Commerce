using Commerce.Business;
using Commerce.Business.Services;
using Commerce.Global;
using Interview.DataAccess;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Commerce.Tests
{
    public class GetBonusTests
    {
        [Fact]
        public void Handle_Positive_bySpentValue()
        {
            var dbContext = new DbContext();
            IAccountService accountService = new AccountService(dbContext);

            var user = dbContext.Users.FirstOrDefault();
            var bonusAccount = dbContext.Accounts.FirstOrDefault(it => it.Id == 0 && it.User.Id == user.Id);
            var accountEUR = dbContext.Accounts.FirstOrDefault(it => it.Currency == Currency.EUR && it.User.Id == user.Id);

            var expectedBonusBalance = 0.0m;
            var expectedBonus = (decimal)Math.Round(10.0 * 1.15 * 0.01, 3);
            expectedBonusBalance += expectedBonus;

            accountService.TopUpAccount(user.Id, accountEUR.Id, 1000.0);
            accountEUR.Balance.ShouldBeEquivalentTo(1000m);
            accountService.ReduceAccount(user.Id, accountEUR.Id, 10.0);
            accountEUR.Balance.ShouldBeEquivalentTo(990m);

            expectedBonus.ShouldBeEquivalentTo(accountService.GetBonus(user.Id, accountEUR.Id, 10.0));
            bonusAccount.Balance.ShouldBeEquivalentTo(expectedBonusBalance);
        }

        [Fact]
        public void Handle_Positive_byOperationsCount()
        {
            var dbContext = new DbContext();
            IAccountService accountService = new AccountService(dbContext);

            var user = dbContext.Users.FirstOrDefault();
            var bonusAccount = dbContext.Accounts.FirstOrDefault(it => it.Id == 0 && it.User.Id == user.Id);
            var accountEUR = dbContext.Accounts.FirstOrDefault(it => it.Currency == Currency.EUR && it.User.Id == user.Id);
            accountEUR.OperationsCount = 10;

            var expectedBonusBalance = 0.0m;
            var expectedBonus = (decimal)Math.Round(10.0 * 1.15 * (0.01 + 0.01), 3);
            expectedBonusBalance += expectedBonus;

            accountService.TopUpAccount(user.Id, accountEUR.Id, 1000.0);
            accountEUR.Balance.ShouldBeEquivalentTo(1000m);
            accountService.ReduceAccount(user.Id, accountEUR.Id, 10.0);
            accountEUR.Balance.ShouldBeEquivalentTo(990m);

            expectedBonus.ShouldBeEquivalentTo(accountService.GetBonus(user.Id, accountEUR.Id, 10.0));
            bonusAccount.Balance.ShouldBeEquivalentTo(expectedBonusBalance);
        }
    }
}
