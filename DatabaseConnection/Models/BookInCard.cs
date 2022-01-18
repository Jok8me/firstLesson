using DatabaseConnection.Models;
using DatabaseConnection.TableService;
using System.Text;

using System.Text;

namespace DatabaseConnection.Models
{
    //Books.id, Books.title, Books.book_status_id, " +
    //$"Books.discount_on_book_id, Books.price, BorrowBook.borrow_start_date, BorrowBook.borrow_end_date "
    public class BookInCard
    {
        public int _Id { get; set; }
        public string _Title { get; set; }
        public string _Author { get; set; }
        public int _StatusId { get; set; }
        public int _DiscountId { get; set; }
        public double _Price { get; set; }
        public DateTime _BorrowStartDate { get; set; }
        public DateTime _BorrowEndDate { get; set; }

        public string _Info { get; set; }

        public BookInCard(int Id, string Title, string Author,int StatusId, int DiscountId, double Price)
        {
            _Id = Id;
            _Title = Title;
            _Author = Author;
            _StatusId = StatusId;
            _DiscountId = DiscountId;
            _Price = Price;
            _Info = "";
        }

        public bool DateIsCorrect()
        {
            DateTime date = DateTime.Now;
            DateTime currentDay = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            bool isCorrect = true;

            if(this._BorrowStartDate > this._BorrowEndDate)
                isCorrect = false;
            if (this._BorrowStartDate < currentDay)
                isCorrect = false;
            if (!((this._BorrowEndDate - this._BorrowStartDate).TotalDays <= (1 * 31)))
                isCorrect = false; // Cant acces to AppConfig.MaxBorrowTimeMonths

            if (!isCorrect)
            {
                StringBuilder stringBuilder = new StringBuilder(this._Info);
                stringBuilder.Append("<br>Incorret date range.</br>");
                this._Info = stringBuilder.ToString();
            }
            return isCorrect;
        }

        public bool DateIsCorrectWithBorrowedBook(BorrowedBook borrowedBook)
        {
            bool isCorrect = true;

            if (this._BorrowStartDate < borrowedBook.borrowEndDate)
                isCorrect = false;

            if (!isCorrect)
            {
                StringBuilder stringBuilder = new StringBuilder(this._Info);
                stringBuilder.Append("<br>Start date must be higher than");
                stringBuilder.Append(borrowedBook.borrowEndDate.ToString());
                stringBuilder.Append("</br>");
                this._Info = stringBuilder.ToString();
            }
            return isCorrect;
        }

        public bool DateIsCorrectWithQueueBooks(HashSet<BookQueue> booksInQueue)
        {
            bool isCorrect = true;

            foreach(BookQueue book in booksInQueue)
            {
                if(this._BorrowStartDate < book._borrowTo)
                {
                    isCorrect = false;
                    StringBuilder stringBuilder = new StringBuilder(this._Info);
                    stringBuilder.Append("<br>Start date must be higher than");
                    stringBuilder.Append(book._borrowTo.ToString());
                    stringBuilder.Append("</br>");
                    this._Info = stringBuilder.ToString();

                }
            }
            return isCorrect;
        }
    }
}
