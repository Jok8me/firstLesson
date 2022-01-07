using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.Models
{
    public class BorrowedBook
    {
        public int id;
        public string title;
        public string author;
        public DateTime borrowStartDate;
        public DateTime borrowEndDate;
        public string bookType;
        public int discountOnItemId;
        public double price;

        public BorrowedBook(int id, string title, string author, DateTime borrowStartDate, DateTime borrowEndDate, string bookType, int discountOnItemId, double price)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.borrowStartDate = borrowStartDate;
            this.borrowEndDate = borrowEndDate;
            this.bookType = bookType;
            this.discountOnItemId = discountOnItemId;
            this.price = price;
        }
    }
}
