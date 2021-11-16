using Commerce.Global;

namespace Commerce.Business
{
    public interface IAccountService
    {
        public string GetBalance(int userId, int accountId);

        public void TopUpAccount(int userId, int accountId, double value);

        public void ReduceAccount(int userId, int accountId, double value);

        public decimal GetBonus(int userId, int accountId, double value);

        public double GetCoefficientToUSD(Currency currency);
    }
}
