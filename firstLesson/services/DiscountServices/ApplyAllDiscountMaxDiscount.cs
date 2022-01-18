using DatabaseConnection.Models.DiscountStrategies;
using firstLesson.models.Books;
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

        public ApplyAllDiscountMaxDiscount(List<IDiscountStrategy> list, Book book)
        {
            List<IDiscountStrategy> tempList = new List<IDiscountStrategy>(list);
            if (book.GetDiscountStrategy() != null)
                tempList.Add(book.GetDiscountStrategy());
            SortedMaxDiscountList = tempList.OrderBy(o => o.calculate(book.price)).ToList();
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
