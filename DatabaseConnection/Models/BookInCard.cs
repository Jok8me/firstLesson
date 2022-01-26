using DatabaseConnection.Models;
using DatabaseConnection.Models.DiscountStrategies;
using DatabaseConnection.TableService;
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
        public int _DiscountType { get; set; }
        public double _DiscountAmount { get; set; }
        public double _Price { get; set; }
        public double _PriceAfterDiscount { get; set; }
        public DateTime _BorrowStartDate { get; set; }
        public DateTime _BorrowEndDate { get; set; }

        public string _Info { get; set; }

        public BookInCard(int Id, string Title, string Author,int StatusId, int DiscountType, double DiscountAmmount, double Price)
        {
            _Id = Id;
            _Title = Title;
            _Author = Author;
            _StatusId = StatusId;
            _DiscountType = DiscountType;
            _DiscountAmount = DiscountAmmount;
            _Price = Price;
            _Info = "";
            _PriceAfterDiscount = 0;

            if (_DiscountAmount != 0)
            {
                IDiscountStrategy discountStrategy;
                switch (_DiscountType)
                {
                    case 0:
                        discountStrategy = new PriceDiscountStrategy(_DiscountAmount);
                        break;
                    case 1:
                        discountStrategy = new PercentageDiscountStrategy(_DiscountAmount);
                        break;
                    default:
                        discountStrategy = new PriceDiscountStrategy(_DiscountAmount);
                        break;
                }
                _PriceAfterDiscount = discountStrategy.calculate(_Price);

            }
        }

        public bool DateIsCorrect()
        {
            bool isCorrect = true;

            if(this._BorrowStartDate > this._BorrowEndDate)
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

        public bool DateHasCorrectRange()
        {
            bool isCorrect = true;

            if (!((this._BorrowEndDate - this._BorrowStartDate).TotalDays <= (1 * 31)))
                isCorrect = false; // Cant acces to AppConfig.MaxBorrowTimeMonths

            if (!isCorrect)
            {
                StringBuilder stringBuilder = new StringBuilder(this._Info);
                stringBuilder.Append("<br>Borrow date cannot be greater than 31 days</br>");
                this._Info = stringBuilder.ToString();
            }
            return isCorrect;
        }

        public bool DateIsCorrectWithBorrowedBook(BorrowedBook borrowedBook)
        {
            bool isCorrect = true;

            if (this._BorrowStartDate < borrowedBook.borrowEndDate)
            {
                isCorrect = false;
                StringBuilder stringBuilder = new StringBuilder(this._Info);
                stringBuilder.Append("<br>Start date must be higher than: ");
                stringBuilder.Append(borrowedBook.borrowEndDate.ToString("dd.MM.yyyy"));
                stringBuilder.Append("</br>");
                this._Info = stringBuilder.ToString();
            }
            return isCorrect;
        }



        public bool BookIsntBorrowedOrInQueueByUser(List<int> borrowedOrInQueueBooks)
        {
            bool isCorrect = true;

            if (borrowedOrInQueueBooks.Contains(this._Id))
            {
                isCorrect = false;
                StringBuilder stringBuilder = new StringBuilder(this._Info);
                stringBuilder.Append("<br>Book is borrowed or in queue.</br>");
                this._Info = stringBuilder.ToString();
            }
            return isCorrect;
        }


        public bool DateIsCorrectWithQueueBooks(HashSet<BookQueue> booksInQueue)
        {
            bool isCorrect = true;

            foreach(BookQueue book in booksInQueue)
            {
                // Start date is between queue dates 
                if (book._borrowFrom < this._BorrowStartDate && this._BorrowStartDate < book._borrowTo)
                {
                    isCorrect = false;
                    StringBuilder stringBuilder = new StringBuilder(this._Info);
                    stringBuilder.Append("<br>Start date must be higher than: ");
                    stringBuilder.Append(book._borrowTo.ToString("dd.MM.yyyy"));
                    stringBuilder.Append("</br>");
                    this._Info = stringBuilder.ToString();
                }
                if(book._borrowFrom < this._BorrowEndDate && this._BorrowEndDate < book._borrowTo)
                {
                    isCorrect = false;
                    StringBuilder stringBuilder = new StringBuilder(this._Info);
                    stringBuilder.Append("<br>End date must be higher than: ");
                    stringBuilder.Append(book._borrowTo.ToString("dd.MM.yyyy"));
                    stringBuilder.Append("</br>");
                    this._Info = stringBuilder.ToString();
                }
            }
            return isCorrect;
        }

        public bool DataIsCurrent()
        {
            DateTime date = DateTime.Now;
            DateTime currentDay = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime borrowStartDate = new DateTime(this._BorrowStartDate.Year, this._BorrowStartDate.Month, this._BorrowStartDate.Day, 0, 0, 0);

            bool isCorrect = true;

            if (currentDay != borrowStartDate)
                isCorrect = false;

            if (!isCorrect)
            {
                StringBuilder stringBuilder = new StringBuilder(this._Info);
                stringBuilder.Append("<br>Borrow date must be current date.</br>");
                this._Info = stringBuilder.ToString();
            }
            return isCorrect;
        }

        public void NoSpaceForBorrowOrQueue()
        {
            StringBuilder stringBuilder = new StringBuilder(this._Info);
            stringBuilder.Append("<br>Queue is full.</br>");
            this._Info = stringBuilder.ToString();
        }

        public void NotImplemented()
        {
            StringBuilder stringBuilder = new StringBuilder(this._Info);
            stringBuilder.Append("<br>Exception not implemented.</br>");
            this._Info = stringBuilder.ToString();
        }
    }
}
