using DatabaseConnection.Models.DiscountStrategies;
using firstLesson.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.models.Books
{
    public class Book
    {
        public string title { get; private set; }
        public string author { get; private set; }
        public string publicationDate { get; private set; }
        private BookStatus bookStatus;
        private IDiscountStrategy _iDiscountStrategy;
        public double price { get; private set; }

        public Book(string title, string author,string publicationDate, BookStatus bookStatus, double price)
        {
            this.title = title;
            this.author = author;   
            this.price = price; 
            this.publicationDate = publicationDate;
            this.bookStatus = bookStatus;
            _iDiscountStrategy = new PriceDiscountStrategy(0);
            this.price = price;
        }

        public void SetDiscountStrategy(IDiscountStrategy iDiscountStrategy)
        {
            _iDiscountStrategy = iDiscountStrategy;
        }
        public IDiscountStrategy GetDiscountStrategy()
        {
            return _iDiscountStrategy;
        }
    }
}
