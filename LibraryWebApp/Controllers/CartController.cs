using DatabaseConnection.TableService;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            BookDBService bookDBService = new BookDBService();
            string cart = HttpContext.Session.GetString("Cart");
            double subtotal = 0;
            double discount = 0;
            double cashPenalty = 0;

            if (cart != null)
            {
                List<int> booksId = HttpContext.Session.GetString("Cart").Split(';').Select(Int32.Parse).ToList();
                List<DatabaseConnection.Models.BookInCard> booksList = bookDBService.getBookByBookIdInIntList(booksId);
                ViewBag.BooksInCart = booksList;
                foreach (var book in booksList)
                {
                    subtotal += book._Price;
                }
            }

            double total = subtotal - discount + cashPenalty;
            ViewBag.Discount = discount;
            ViewBag.CashPenalty = cashPenalty *(-1);
            ViewBag.Subtotal = subtotal;
            ViewBag.Total = total;
            return View();
        }

        public IActionResult Checkout()
        {
            BookDBController bookDBController = new BookDBController();
            int userId = (int)HttpContext.Session.GetInt32("userId");
            if(HttpContext.Session.GetString("Cart") != null)
            {
                List<int> booksId = HttpContext.Session.GetString("Cart").Split(';').Select(Int32.Parse).ToList();
                foreach (var bookId in booksId)
                    bookDBController.BorrowBookByUser(bookId, userId);
            }

            HttpContext.Session.SetString("Cart", "");
            HttpContext.Session.Remove("Cart");
            ViewBag.BooksInCart = "";

            return View("Views/Home/Index.cshtml");
        }
    }
}
