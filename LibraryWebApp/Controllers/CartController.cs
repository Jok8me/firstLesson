using DatabaseConnection.Models;
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

            if (cart != null && cart != "")
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

        public IActionResult RemoveFromCart(int bookToDelete)
        {
            BookDBService bookDBService = new BookDBService();
            string cart = HttpContext.Session.GetString("Cart");

            int itemInCart = (int)HttpContext.Session.GetInt32("CartCount");
            if(itemInCart > 0) itemInCart = itemInCart - 1;

            double subtotal = 0;
            double discount = 0;
            double cashPenalty = 0;

            if (cart != null || cart != "")
            {
                List<int> booksId = HttpContext.Session.GetString("Cart").Split(';').Select(Int32.Parse).ToList();
                booksId.Remove(bookToDelete);
                if(booksId.Count > 0)
                {
                    List<DatabaseConnection.Models.BookInCard> booksList = bookDBService.getBookByBookIdInIntList(booksId);
                    ViewBag.BooksInCart = booksList;
                    foreach (var book in booksList)
                    {
                        subtotal += book._Price;
                    }
                    cart = string.Join(";", booksId);
                }
                else
                {
                    cart = "";
                }
            }

            HttpContext.Session.SetString("Cart", cart);
            HttpContext.Session.SetInt32("CartCount", itemInCart);
            double total = subtotal - discount + cashPenalty;
            ViewBag.Discount = discount;
            ViewBag.CashPenalty = cashPenalty * (-1);
            ViewBag.Subtotal = subtotal;
            ViewBag.Total = total;
            return View("Index");
        }


        public IActionResult RemoveAllFromCart()
        {
            double subtotal = 0;
            double discount = 0;
            double cashPenalty = 0;


            double total = subtotal - discount + cashPenalty;
            ViewBag.Discount = discount;
            ViewBag.CashPenalty = cashPenalty;
            ViewBag.Subtotal = subtotal;
            ViewBag.Total = total;

            HttpContext.Session.SetInt32("CartCount", 0);
            HttpContext.Session.Remove("Cart");
            return View("Index");
        }


        [HttpPost]
        public IActionResult Checkout()
        {
            BookDBController bookDBController = new BookDBController();
            QueueDBService queueDBService = new QueueDBService();

            int userId = (int)HttpContext.Session.GetInt32("userId");

            if(HttpContext.Session.GetString("Cart") != null)
            {
                List<int> booksId = HttpContext.Session.GetString("Cart").Split(';').Select(Int32.Parse).ToList();
                HashSet<BorrowedBook> borrowedBooks = bookDBController.GetBorrowedBookByBooksID(booksId);

                foreach (var bookId in booksId)
                {
                    if (borrowedBooks.Any(book => book.id == bookId))
                    {
                        List<BookQueue> bookQueues = new List<BookQueue>();
                        bookQueues = queueDBService.GetBookQueue(bookId);





                        BorrowedBook borrowedBook = borrowedBooks.Where(i => i.id == bookId).FirstOrDefault();
                    }
                    else
                    {
                        bookDBController.BorrowBookByUser(bookId, userId);
                    }
                }
            }

            HttpContext.Session.SetString("Cart", "");
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.SetInt32("CartCount", 0);
            ViewBag.BooksInCart = "";

            List<DatabaseConnection.Models.BookDetails> bookList = bookDBController.GetBooks();
            ViewBag.Books = bookList;
            return View("/Views/BooksSearching/Index.cshtml");
        }
    }
}
