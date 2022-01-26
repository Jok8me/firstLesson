using DatabaseConnection.UsersTableServices;
using Microsoft.AspNetCore.Mvc;
using DatabaseConnection.Models;
using DatabaseConnection.TableService;
using LibraryWebApp.Services;

namespace LibraryWebApp.Controllers
{
    public class EmployeePanelController : Controller
    {
        public IActionResult Index()
        {
            UserDBService userDBService = new UserDBService();
            List<BookQueue> inQueues = new List<BookQueue>();

            ViewBag.Users = userDBService.GetUsers();
            ViewBag.BookInQueue = inQueues;
            ViewBag.UserId = -1;
            return View("/Views/EmployeePanel/Index.cshtml");
        }


        public IActionResult GetBooksInQueueByUserId(int userId)
        {
            UserDBService userDBService = new UserDBService();
            QueueDBService queueDBService = new QueueDBService();
            List<BookQueue> inQueues = queueDBService.GetBooksQueueByUserID(userId);
            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);


            SessionStorageServices.Set<int>(HttpContext.Session, "userId", userId);
            ViewBag.Users = userDBService.GetUsers();

            foreach (BookQueue bookQueue in inQueues)
            {
                if (bookQueue._borrowFrom <= dateTime && bookQueue._bookStatus == 0)
                    bookQueue._canBorrow = true;
            }


            ViewBag.BookInQueue = inQueues;
            ViewBag.UserId = userId;
            return View("/Views/EmployeePanel/Index.cshtml");
        }

        public IActionResult Borrow(int bookId, int userId)
        {
            QueueDBService queueDBService = new QueueDBService();
            UserDBService userDBService = new UserDBService();
            List<BookQueue> bookQueues = queueDBService.GetBooksQueueByUserID(userId);
            ViewBag.BookInQueue = bookQueues;
            return View("/Views/EmployeePanel/Index.cshtml");
        }
    }
}
