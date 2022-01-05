using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.models.Books.DiscountStrategies
{
    public class PriceDiscountStrategy : IDiscountStrategy
    {
        private double _discount;
        public PriceDiscountStrategy(double discount)
        {
            _discount = discount;
        }
        public double calculate(double price)
        {
            return price - _discount;
        }

    }
}
