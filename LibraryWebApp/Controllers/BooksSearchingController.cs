using LibraryWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    public class BooksSearchingController : Controller
    {
        public IActionResult Index()
        {
            BookDBController bookDBController = new BookDBController();
            List<DatabaseConnection.Models.BookDetails> bookList = bookDBController.GetBooks();
            ViewBag.Books = bookList;
            return View();
        }

        public IActionResult BookOrLendBook(int bookId)
        {
            int userId = (int)HttpContext.Session.GetInt32("userId");
            BookDBController bookDBController = new BookDBController();
            bookDBController.BorrowBookByUser(bookId, userId);

            List<DatabaseConnection.Models.BookDetails> bookList = bookDBController.GetBooks();
            ViewBag.Books = bookList;
            return View("Index");
        }
    }
}
