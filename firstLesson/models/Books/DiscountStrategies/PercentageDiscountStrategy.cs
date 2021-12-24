using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.models.Books.DiscountStrategies
{
    internal class PercentageDiscountStrategy : IDiscountStrategy
    {
        public double calculate(double price, double discount)
        {
            return price * discount / 100;
        }
    }
}
