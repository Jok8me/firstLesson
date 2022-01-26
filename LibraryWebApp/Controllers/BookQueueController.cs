using DatabaseConnection.Models;
using DatabaseConnection.TableService;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    public class BookQueueController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.BookIdError = -1;
            int userId = (int)HttpContext.Session.GetInt32("userId");

            QueueDBService queueDBService = new QueueDBService();
            List<BookQueue> bookQueues = queueDBService.GetBooksQueueByUserID(userId);
            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0,0,0);

            foreach (BookQueue bookQueue in bookQueues)
            {
                if(bookQueue._borrowFrom <= dateTime && bookQueue._bookStatus == 0)
                    bookQueue._canBorrow = true;
            }

            ViewBag.BookInQueue = bookQueues;
            return View("/Views/BookQueue/Index.cshtml");
        }

        public IActionResult Borrow(int bookId)
        {
            ViewBag.BookIdError = -1;
            int userId = (int)HttpContext.Session.GetInt32("userId");

            QueueDBService queueDBService = new QueueDBService();
            List<BookQueue> bookQueues = queueDBService.GetBooksQueueByUserID(userId);
            ViewBag.BookInQueue = bookQueues;
            return View("/Views/BookQueue/Index.cshtml");
        }


        public IActionResult Change(int bookId, DateTime newStartDate, DateTime newEndDate)
        {
            int userId = (int)HttpContext.Session.GetInt32("userId");

            QueueDBService queueDBService = new QueueDBService();
            List<BookQueue> bookQueues = queueDBService.GetBooksQueueByUserID(userId);

            if ((DateService.DateIsCorrect(newStartDate, newEndDate)) && (DateService.DateIsCorrectWithQueueBooks(bookQueues.Where(x => x._bookId == bookId).First()._bookDateRanges, newStartDate, newEndDate)))
            {
                bookQueues.Where(x => x._bookId == bookId).Select(x => { x._borrowFrom = newStartDate; x._borrowTo = newEndDate; return x; }).ToList();
                queueDBService.UpdateDateBookInQueue(bookId, userId, newStartDate, newEndDate);
            }
            else
            {
                ViewBag.ErrorLog = "Incorrect date input.";
                ViewBag.BookIdError = bookId;
            }

            ViewBag.BookInQueue = bookQueues;
            return View("/Views/BookQueue/Index.cshtml");
        }

        public IActionResult Remove(int bookId)
        {
            ViewBag.BookIdError = -1;
            int userId = (int)HttpContext.Session.GetInt32("userId");
            QueueDBService queueDBService = new QueueDBService();
            queueDBService.RemoveBookFromQueue(bookId, userId);

            List<BookQueue> bookQueues = queueDBService.GetBooksQueueByUserID(userId);
            ViewBag.BookInQueue = bookQueues;
            return View("/Views/BookQueue/Index.cshtml");
        }

    }
}
