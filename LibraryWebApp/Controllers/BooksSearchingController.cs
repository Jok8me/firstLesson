using LibraryWebApp.Services;
using Microsoft.AspNetCore.Http;
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

        public IActionResult SearchBookByCategories(int bookType, List<int> bookCategory)
        {
            BookDBController bookDBController = new BookDBController();
            List<DatabaseConnection.Models.BookDetails> bookList = bookDBController.GetBooksByTypeAndCategory(bookType, bookCategory);
            ViewBag.Books = bookList;
            return View("Index");
        }

        public IActionResult AddToCart(int bookId)
        {
            BookDBController bookDBController = new BookDBController();

            int cartCount = (int)HttpContext.Session.GetInt32("CartCount");
            HttpContext.Session.SetInt32("CartCount", cartCount++);

            if (HttpContext.Session.GetString("Cart") == null)
            {
                HttpContext.Session.SetString("Cart", bookId.ToString());
            }
            else
            {
                List<int> booksId = HttpContext.Session.GetString("Cart").Split(';').Select(Int32.Parse).ToList();
                booksId.Add(bookId);
                string newBooksId = string.Join(";", booksId);
                HttpContext.Session.SetString("Cart", newBooksId);
            }
            string cart = HttpContext.Session.GetString("Cart");
            List<DatabaseConnection.Models.BookDetails> bookList = bookDBController.GetBooks();
            ViewBag.Books = bookList;
            return View("Index");
        }
    }
}
