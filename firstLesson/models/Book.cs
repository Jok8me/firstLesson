using firstLesson.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.models
{
    public class Book
    {
        private string title;
        private string author;
        private double price;
        private string publicationDate;
        private BookStatus bookStatus;
        //private double discount;

        public Book(string title, string author ,double price ,string publicationDate, BookStatus bookStatus)
        {
            this.title = title;
            this.author = author;   
            this.price = price; 
            this.publicationDate = publicationDate;
            this.bookStatus = bookStatus;
        }
    }
}
