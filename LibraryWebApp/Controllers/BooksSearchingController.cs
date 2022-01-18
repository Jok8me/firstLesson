using DatabaseConnection.TableService;
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
            BorrowDBService borrowDBService = new BorrowDBService();
            List<DatabaseConnection.Models.BookDetails> bookList = bookDBController.GetBooks();
            ViewBag.KeepedCount = borrowDBService.GetNumberOfNotReturnedBooks((int)HttpContext.Session.GetInt32("userId"));
            ViewBag.Books = bookList;
            return View("/Views/BooksSearching/Index.cshtml");
        }

        public IActionResult SearchBookByCategoriesAndInput(int bookType, List<int> bookCategory, string searchInput)
        {
            BorrowDBService borrowDBService = new BorrowDBService();
            ViewBag.KeepedCount = borrowDBService.GetNumberOfNotReturnedBooks((int)HttpContext.Session.GetInt32("userId"));
            BookDBController bookDBController = new BookDBController();
            List<DatabaseConnection.Models.BookDetails> bookList;
            if (searchInput == null)
                searchInput = "";

            bookList = bookDBController.GetBooksByTypeAndCategoryAndSearchInput(bookType, bookCategory, searchInput);
            ViewBag.Books = bookList;
            return View("/Views/BooksSearching/Index.html");
        }

        public IActionResult AddToCart(int bookId)
        {
            BorrowDBService borrowDBService = new BorrowDBService();
            ViewBag.KeepedCount = borrowDBService.GetNumberOfNotReturnedBooks((int)HttpContext.Session.GetInt32("userId"));
            BookDBController bookDBController = new BookDBController();
            int cartCount = (int)HttpContext.Session.GetInt32("CartCount");
            cartCount = cartCount + 1;
            HttpContext.Session.SetInt32("CartCount", cartCount);

            if (HttpContext.Session.GetString("Cart") == null || HttpContext.Session.GetString("Cart") == "")
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
            return View("/Views/BooksSearching/Index.cshtml");
        }

        public IActionResult BorrowNow(int bookId)
        {
            BorrowDBService borrowDBService = new BorrowDBService();
            ViewBag.KeepedCount = borrowDBService.GetNumberOfNotReturnedBooks((int)HttpContext.Session.GetInt32("userId"));
            BookDBController bookDBController = new BookDBController();
            BookDBService bookDBService = new BookDBService();

            int cartCount = (int)HttpContext.Session.GetInt32("CartCount");
            cartCount = cartCount + 1;

            HttpContext.Session.SetInt32("CartCount", cartCount);

            if (HttpContext.Session.GetString("Cart") == null || HttpContext.Session.GetString("Cart") == "")
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
            ViewBag.CashPenalty = cashPenalty * (-1);
            ViewBag.Subtotal = subtotal;
            ViewBag.Total = total;

            return View("/Views/Cart/Index.cshtml");
        }
    }
}
