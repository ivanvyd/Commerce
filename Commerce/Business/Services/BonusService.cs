using System;

namespace Commerce.Business.Services
{
    public class BonusService : IBonusService
    { 
        public decimal GetBonus(decimal value)
        {
            return 0.0m;
        }
    }

    public class BonusDecorator : IBonusService
    {
        private readonly IBonusService Bonus;

        public BonusDecorator(IBonusService bonus)
        {
            Bonus = bonus;
        }

        public virtual decimal GetBonus(decimal value)
        {
            return Bonus.GetBonus(value);
        }
    }

    public class BonusBySpentValueDecorator : BonusDecorator
    {
        public BonusBySpentValueDecorator(IBonusService bonus) : base(bonus)
        { }

        public override decimal GetBonus(decimal value)
        {
            var bonus = base.GetBonus(value);
            bonus += value * 0.01m;
            return bonus;
        }
    }

    public class BonusByOperationsCountDecorator : BonusDecorator
    {
        public BonusByOperationsCountDecorator(IBonusService bonus) : base(bonus)
        { }

        public override decimal GetBonus(decimal value)
        {
            var bonus = base.GetBonus(value);
            bonus += value * 0.01m;
            return bonus;
        }
    }
}
