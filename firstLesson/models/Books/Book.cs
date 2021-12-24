using firstLesson.enums;
using firstLesson.models.Books.DiscountStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.models.Books
{
    public class Book
    {
        private string title;
        private string author;
        private double price;
        private string publicationDate;
        private BookStatus bookStatus;
        IDiscountStrategy iDiscountStrategy;

        public Book(string title, string author ,double price ,string publicationDate, BookStatus bookStatus)
        {
            this.title = title;
            this.author = author;   
            this.price = price; 
            this.publicationDate = publicationDate;
            this.bookStatus = bookStatus;
        }
        public Book(string title, string author, double price, string publicationDate, BookStatus bookStatus, IDiscountStrategy iDiscountStrategy) : this(title, author, price, publicationDate, bookStatus)
        {
            this.iDiscountStrategy = iDiscountStrategy;
        }

        public void SetStrategy(IDiscountStrategy iDiscountStrategy)
        {
            this.iDiscountStrategy = iDiscountStrategy;
        }

        public double GetPriceAfterDiscount(double price, double discount)
        {
            return iDiscountStrategy.calculate(price, discount);
        }


    }
}
