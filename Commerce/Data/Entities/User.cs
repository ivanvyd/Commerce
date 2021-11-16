using System.Collections.Generic;

namespace Commerce.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public List<Account> Accounts { get; set; }

        public Account BonusAccount { get; set; }
    }
}
