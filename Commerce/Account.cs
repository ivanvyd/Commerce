using Commerce.Global;

namespace Commerce.Accounts
{
    public class Account
    {
        public Account(int userId, double balance, Currency currency)
        {
            Id = 0;
            UserId = userId;
            Balance = balance;
            Currency = currency;
            OperationsCount = 0;
        }

        private int id;

        public int Id { get { return id; } set { id = Increment.Id++; } }

        public int UserId { get; set; }

        public double Balance { get; set; }

        public Currency Currency { get; set; }

        public int OperationsCount { get; set; }

        public string GetBalance()
        {
            return $"Account #{Id}: {Balance} {Currency}";
        }
    }
}
