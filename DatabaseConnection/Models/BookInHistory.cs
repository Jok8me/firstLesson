using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.Models
{
    public class BookInHistory
    {
        // SELECT b.title, a.name, a.surname, bb.borrow_start_date, bb.borrow_end_date, b.price
        public int _bookId { get; set; }
        public string _title { get; set; }
        public string _author { get; set; }
        public DateTime _publicationDate { get; set; }
        public string _bookType { get; set; }
        public string _bookCategory { get; set; }
        public string _bookDescription { get; set; }
        public DateTime _startDate { get; set; }
        public DateTime _endDate   { get; set; }
        public double _price { get; set; }
        public DateTime _returnDate { get; set; }

        public BookInHistory(int bookId,string title,string authorName, string authorSurname,DateTime publicationDate,string bookType,string bookCategory,string bookDescription,DateTime startDate,DateTime endDate,double price, DateTime returnDate)
        {
        _bookId = bookId;
        _title = title;
        _author = authorName + " " + authorSurname;
        _publicationDate = publicationDate;
        _bookType = bookType;
        _bookCategory = bookCategory;
        _bookDescription = bookDescription;
        _startDate = startDate;
        _endDate = endDate;
        _price = price;
        _returnDate = returnDate;
        }
    }
}
