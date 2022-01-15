using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.Models
{
    public  class BookQueue
    {
        public int _id { get; set; }
        public int _bookId { get; set; }
        public DateTime _borrowFrom { get; set; }
        public DateTime _borrowTo { get; set; }

        public BookQueue(int id, int bookId, DateTime borrowFrom, DateTime borrowTo)
        {
            _id = id;
            _bookId = bookId;
            _borrowFrom = borrowFrom;
            _borrowTo = borrowTo;
        }
    }
}
