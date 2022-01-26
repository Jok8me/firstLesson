using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.Models
{
    public  class BookQueue
    {
        public int _bookId { get; set; }
        public int _userId { get; set; }
        public string _title { get; set; }
        public string _author { get; set; }
        public DateTime _borrowFrom { get; set; }
        public DateTime _borrowTo { get; set; }
        public int _bookStatus { get; set; }
        public bool _canBorrow { get; set; }
        public List<BookDateRange> _bookDateRanges { get; set; }

        public BookQueue(int bookId, int userId, DateTime borrowFrom, DateTime borrowTo)
        {
            _bookDateRanges = new List<BookDateRange>();

            _bookId = bookId;
            _userId = userId;
            _borrowFrom = borrowFrom;
            _borrowTo = borrowTo;
            _bookStatus = 0;
            _canBorrow = false;
            _title = "";
            _author = "";
        }

        public void setBookStatus(int bookStatus)
        {
            _bookStatus = bookStatus;
        }
        public void setTitleAndAuthor(string title, string author)
        {
            _title = title;
            _author = author;
        }

        public void addDatesToDateRanges(BookDateRange bookDateRange)
        {
            _bookDateRanges.Add(bookDateRange);
        }
    }
}
