using DatabaseConnection.Models;
using DatabaseConnection.TableService;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    public class BookQueueController : Controller
    {
        public IActionResult Index()
        {
            int userId = (int)HttpContext.Session.GetInt32("userId");

            QueueDBService queueDBService = new QueueDBService();
            HashSet<BookQueue> bookQueues = queueDBService.GetBooksQueueByUserID(userId);
            ViewBag.BookInQueue = bookQueues;
            return View();
        }

        public IActionResult Borrow(int bookId)
        {
            int userId = (int)HttpContext.Session.GetInt32("userId");

            QueueDBService queueDBService = new QueueDBService();
            HashSet<BookQueue> bookQueues = queueDBService.GetBooksQueueByUserID(userId);
            ViewBag.BookInQueue = bookQueues;
            return View();
        }
    }
}
