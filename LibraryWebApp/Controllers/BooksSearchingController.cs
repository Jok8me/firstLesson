using DatabaseConnection.Models;
using DatabaseConnection.TableService;
using DatabaseConnection.UsersTableServices;
using firstLesson.resources;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web.Helpers;

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
            ViewBag.Books = BookService.DetailsBooksInViewBag();
            ViewBag.BOTD = bookDBController.GetBOTD();
            return View("/Views/BooksSearching/Index.cshtml");
        }

        public IActionResult SearchBookByCategoriesAndInput(int bookType, List<int> bookCategory, string searchInput)
        {
            BorrowDBService borrowDBService = new BorrowDBService();
            BookDBController bookDBController = new BookDBController();
            List<DatabaseConnection.Models.BookDetails> bookList = BookService.DetailsBooksInViewBag();


            if (searchInput == null) searchInput = "";
            bookList = SearchService.Search(bookList,bookType, bookCategory, searchInput);

            SessionStorageServices.Set<int>(HttpContext.Session, "bookType", bookType);
            SessionStorageServices.Set<List<int>>(HttpContext.Session, "bookCategory", bookCategory);
            SessionStorageServices.Set<string>(HttpContext.Session, "searchInput", searchInput);
            ViewBag.Books = bookList;
            ViewBag.BOTD = bookDBController.GetBOTD();
            ViewBag.KeepedCount = borrowDBService.GetNumberOfNotReturnedBooks((int)HttpContext.Session.GetInt32("userId"));
            return View("/Views/BooksSearching/Index.cshtml");
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
            ViewBag.Books = BookService.DetailsBooksInViewBag();
            ViewBag.BOTD = bookDBController.GetBOTD();
            return View("/Views/BooksSearching/Index.cshtml");
        }

        public IActionResult BorrowNow(int bookId)
        {
            UserDBService userDBService = new UserDBService();
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
            ViewBag.Books = BookService.DetailsBooksInViewBag();
            ViewBag.BOTD = bookDBController.GetBOTD();

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


            //To Method
            if (!String.IsNullOrEmpty(cart))
            {
                List<int> booksId = HttpContext.Session.GetString("Cart").Split(';').Select(Int32.Parse).ToList();
                List<DatabaseConnection.Models.BookInCard> booksList = bookDBService.getBookByBookIdInIntList(booksId);
                HashSet<BorrowedBook> borrowedBooks = bookDBController.GetBorrowedBooksByBooksID(booksId);
                HashSet<BookQueue> booksInQueue = bookDBController.GetBookQueueByBooksID(booksId);

                foreach (BookInCard book in booksList)
                {
                    book._BorrowStartDate = DateTime.Now;
                    book._BorrowEndDate = DateTime.Now.AddMonths(AppConfig.MaxBorrowTimeMonths);

                    foreach (BorrowedBook borrowedBook in borrowedBooks)
                    {
                        if (borrowedBook.id == book._Id)
                        {
                            book._BorrowStartDate = borrowedBook.borrowEndDate;
                            book._BorrowEndDate = book._BorrowStartDate.AddMonths(AppConfig.MaxBorrowTimeMonths);
                            borrowedBooks.Remove(borrowedBook);
                        }
                    }

                    foreach (BookQueue bookInQueue in booksInQueue)
                    {
                        if (bookInQueue._bookId == book._Id)
                        {
                            book._BorrowStartDate = bookInQueue._borrowTo;
                            book._BorrowEndDate = book._BorrowStartDate.AddMonths(AppConfig.MaxBorrowTimeMonths);
                            booksInQueue.Remove(bookInQueue);
                        }
                    }

                }


                BookOfTheDay bookOfTheDay = bookDBController.GetBOTD();
                if (bookOfTheDay != null)
                {
                    booksList.Where(book => book._Id == bookOfTheDay._id).Select(y => y._PriceAfterDiscount = bookOfTheDay._priceAfterDiscount).ToList();
                }


                ViewBag.BooksInCart = booksList;
                foreach (var book in booksList)
                {
                    subtotal += book._Price;
                    if (book._PriceAfterDiscount > 0)
                        discount += book._Price - book._PriceAfterDiscount;
                }
            }

            ViewBag.Users = userDBService.GetUsers();
            double total = subtotal - discount + cashPenalty;
            ViewBag.Discount = discount;
            ViewBag.CashPenalty = cashPenalty;
            ViewBag.Subtotal = subtotal;
            ViewBag.Total = total;

            return View("/Views/Cart/Index.cshtml");
        }
    }
}
