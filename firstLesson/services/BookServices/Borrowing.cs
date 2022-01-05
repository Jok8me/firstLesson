using firstLesson.models.Books;
using firstLesson.models.Books.DiscountStrategies;
using firstLesson.models.Users;
using firstLesson.services.DiscountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.services.BookServices
{
    internal class Borrowing
    {
        private User _user;
        private Book _book;
        private List<IDiscountStrategy> _listStandardUserDiscount;

        public Borrowing(User user, Book book, List<IDiscountStrategy> listStandardUserDiscount)
        {
            _user = user;
            _book = book;
            _listStandardUserDiscount = listStandardUserDiscount;
        }

        public void Borrow()
        {
            Console.WriteLine("(Min)User: " + _user.getLogin()
                            + " Book: " + _book.title
                            + " price: " + new ApplyAllDiscountMinDiscount(_listStandardUserDiscount, _book).calculate(_book.price));
                Console.WriteLine("(Max)User: " + _user.getLogin()
                                + " Book: " + _book.title
                                + " price: " + new ApplyAllDiscountMaxDiscount(_listStandardUserDiscount, _book).calculate(_book.price));


        }
    }
}
