using DatabaseConnection.Models;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryWebApp.Controllers
{
    public class BorrowHistoryController : Controller
    {

        public IActionResult Index()
        {
            BookDBController bookDBController = new BookDBController();
            int userId = (int)HttpContext.Session.GetInt32("userId");

            List<BookInHistory> bookInHistories = bookDBController.GetBooksHistoryByUserId(userId);
            ViewBag.BookInHistory = bookInHistories;
            return View();
        }

    }
}
