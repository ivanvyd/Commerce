using Commerce.Accounts;
using Commerce.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Bonuses
{
    //static class Bonus
    //{
    //    public static void GetBonusBySpentValue(this Account bonusAccount, double value)
    //    {
    //        bonusAccount.Balance += value;
    //    }

    //    public static void GetBonusByOperationsCount(this Account bonusAccount, double value)
    //    {
    //        bonusAccount.Balance += value;
    //    }
    //}

    public interface IBonus
    {
        double GetBonus(double value);
    }

    public class Bonus : IBonus
    { 
        public double GetBonus(double value)
        {
            return 0.0;
        }
    }

    public class BonusDecorator : IBonus
    {
        private readonly IBonus Bonus;

        public BonusDecorator(IBonus bonus)
        {
            Bonus = bonus;
        }

        public virtual double GetBonus(double value)
        {
            return Bonus.GetBonus(value);
        }
    }

    public class BonusBySpentValueDecorator : BonusDecorator
    {
        public BonusBySpentValueDecorator(IBonus bonus) : base(bonus)
        { }

        public override double GetBonus(double value)
        {
            var bonus = base.GetBonus(value);
            bonus += value * 0.1;
            return bonus;
        }
    }

    public class BonusByOperationsCountDecorator : BonusDecorator
    {
        public BonusByOperationsCountDecorator(IBonus bonus) : base(bonus)
        { }

        public override double GetBonus(double value)
        {
            var bonus = base.GetBonus(value);
            bonus += value * 0.1;
            return bonus;
        }
    }
}
