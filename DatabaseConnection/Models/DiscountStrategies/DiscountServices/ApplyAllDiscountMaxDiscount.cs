using DatabaseConnection.Models.DiscountStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.services.DiscountServices
{
    public class ApplyAllDiscountMaxDiscount : IDiscountStrategy
    {
        private List<IDiscountStrategy> SortedMaxDiscountList;

        public ApplyAllDiscountMaxDiscount(List<IDiscountStrategy> list, double price)
        {
            List<IDiscountStrategy> tempList = new List<IDiscountStrategy>(list);
            SortedMaxDiscountList = tempList.OrderBy(o => o.calculate(price)).ToList();
        }

        public double calculate(double price)
        {
            double initialPrice = price;
            foreach (IDiscountStrategy element in SortedMaxDiscountList)
            {
                initialPrice = element.calculate(initialPrice);
            }
            return initialPrice;
        }
    }
}
