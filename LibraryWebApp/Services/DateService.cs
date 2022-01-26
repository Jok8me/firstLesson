using DatabaseConnection.Models;

namespace LibraryWebApp.Services
{
    public  static class DateService
    {
        public static bool IsEmpty<T>(HashSet<T> hashSetObject)
        {
            return hashSetObject.Count == 0;
        }

        public static bool CountLowerThanMax<T>(HashSet<T> hashSetObject)
        {
            return hashSetObject.Count < 3;
        }

        public static bool HashSetIsFull<T>(HashSet<T> hashSetObject)
        {
            return hashSetObject.Count == 3;
        }

        public static bool DateIsCorrect(DateTime _BorrowStartDate, DateTime _BorrowEndDate)
        {
            bool isCorrect = true;

            if (_BorrowStartDate > _BorrowEndDate)
                isCorrect = false;
            if (!((_BorrowEndDate - _BorrowStartDate).TotalDays <= (1 * 31)))
                isCorrect = false; // Cant acces to AppConfig.MaxBorrowTimeMonths
            return isCorrect;
        }

        public static bool DateHasCorrectRange(DateTime _BorrowStartDate, DateTime _BorrowEndDate)
        {
            bool isCorrect = true;

            if (!((_BorrowEndDate - _BorrowStartDate).TotalDays <= (1 * 31)))
                isCorrect = false; // Cant acces to AppConfig.MaxBorrowTimeMonths
            return isCorrect;
        }

        public static bool DateIsCorrectWithQueueBooks(List<BookDateRange> bookDateRanges, DateTime newStartDate, DateTime newEndDate)
        {
            bool isCorrect = true;

            foreach(BookDateRange book in bookDateRanges)
            {
                if (book._startDate < newStartDate && book._endDate > newStartDate)
                {
                    isCorrect = false;
                }
                else if(book._startDate < newEndDate && book._endDate > newEndDate)
                {
                    isCorrect = false;
                }
            }
            return isCorrect;
        }

        public static bool DataIsCurrent(DateTime _BorrowStartDate)
        {
            bool isCorrect = true;
            DateTime date = DateTime.Now;
            DateTime currentDay = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime borrowStartDate = new DateTime(_BorrowStartDate.Year, _BorrowStartDate.Month, _BorrowStartDate.Day, 0, 0, 0);

            if (currentDay != borrowStartDate)
                isCorrect = false;

            return isCorrect;
        }
    }
}
