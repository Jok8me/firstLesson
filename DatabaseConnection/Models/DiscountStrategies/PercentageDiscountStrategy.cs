using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.Models.DiscountStrategies
{
    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private double _discount;
        public PercentageDiscountStrategy(double discount)
        {
            _discount = discount;
        }
        public double calculate(double price)
        {
            return price - price * _discount / 100;
        }
    }
}
