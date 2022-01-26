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
            ViewBag.CurrentDate = DateTime.Now;
            return View();
        }

        [HttpPost]
        public IActionResult returnBookByBookIdAndUserId(int bookId, int selected_rating)
        {
            BookDBController bookDBController = new BookDBController();
            int userId = HttpContext.Session.GetInt32("userId") ?? 0;
            bookDBController.ReturnBookByBookIdAndUserId(bookId, userId, selected_rating);
            List<DatabaseConnection.Models.BorrowedBook> bookList = bookDBController.GetBooksCurrentBorrowedByUserId(userId);
            ViewBag.Books = bookList;
            ViewBag.CurrentDate = DateTime.Now;
            return View("Index");
        }
    }
}
