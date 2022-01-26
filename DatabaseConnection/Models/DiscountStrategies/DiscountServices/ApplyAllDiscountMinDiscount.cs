using DatabaseConnection.Models.DiscountStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.services.DiscountServices
{
    public class ApplyAllDiscountMinDiscount : IDiscountStrategy
    {
        private List<IDiscountStrategy> SortedMinDiscountList;

        public ApplyAllDiscountMinDiscount(List<IDiscountStrategy> list, double price)
        {
            List<IDiscountStrategy> tempList = new List<IDiscountStrategy>(list);
            SortedMinDiscountList = tempList.OrderByDescending(o => o.calculate(price)).ToList();
        }

        public double calculate(double price)
        {
            double initialPrice = price;
            for(int i = 0; i < SortedMinDiscountList.Count; i++)
            {
                initialPrice = SortedMinDiscountList[i].calculate(initialPrice);
            }
            return initialPrice;
        }
    }
}
