using LibraryWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    public class CurrentBorrowedController : Controller
    {
        public IActionResult Index()
        {
            BookDBController bookDBController = new BookDBController();
            int userId = HttpContext.Session.GetInt32("userId") ?? 0;
            List<DatabaseConnection.Models.BorrowedBook> bookList = bookDBController.GetBooksCurrentBorrowedByUserId(userId);
            ViewBag.Books = bookList;
            return View();
        }

        [HttpPost]
        public IActionResult returnBookByBookIdAndUserId(int bookId)
        {
            BookDBController bookDBController = new BookDBController();
            int userId = HttpContext.Session.GetInt32("userId") ?? 0;
            bookDBController.ReturnBookByBookIdAndUserId(bookId, userId);
            List<DatabaseConnection.Models.BorrowedBook> bookList = bookDBController.GetBooksCurrentBorrowedByUserId(userId);
            ViewBag.Books = bookList;
            return View("Index");
        }
    }
}
