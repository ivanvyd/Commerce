using Commerce.Global;

namespace Commerce.Data.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public Currency Currency { get; set; }

        public int OperationsCount { get; set; }
    }
}
