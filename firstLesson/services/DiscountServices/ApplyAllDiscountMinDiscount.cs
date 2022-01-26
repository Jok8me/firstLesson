using DatabaseConnection.Models.DiscountStrategies;
using firstLesson.models.Books;
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

        public ApplyAllDiscountMinDiscount(List<IDiscountStrategy> list, Book book)
        {
            List<IDiscountStrategy> tempList = new List<IDiscountStrategy>(list);
            if (book.GetDiscountStrategy() != null)
                tempList.Add(book.GetDiscountStrategy());
            SortedMinDiscountList = tempList.OrderByDescending(o => o.calculate(book.price)).ToList();
        }

        public ApplyAllDiscountMinDiscount(List<IDiscountStrategy> list)
        {
            List<IDiscountStrategy> tempList = new List<IDiscountStrategy>(list);
            SortedMinDiscountList = tempList;
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
