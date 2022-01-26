using DatabaseConnection.Models.DiscountStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.services.DiscountServices
{
    public class ApplyAllDiscount : IDiscountStrategy
    {

        private List<IDiscountStrategy> SortedMaxDiscountList;

        public ApplyAllDiscount(List<IDiscountStrategy> list)
        {
            SortedMaxDiscountList = list;
        }
        public double calculate(double price)
        {
            double initialPrice = price;
            SortedMaxDiscountList.ForEach(o => initialPrice = o.calculate(initialPrice));
            return initialPrice;
        }
    }
}