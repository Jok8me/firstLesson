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
        public DateTime _borrowFrom { get; set; }
        public DateTime _borrowTo { get; set; }

        public BookQueue(int bookId, int userId, DateTime borrowFrom, DateTime borrowTo)
        {
            _bookId = bookId;
            _userId = userId;
            _borrowFrom = borrowFrom;
            _borrowTo = borrowTo;
        }
    }
}
