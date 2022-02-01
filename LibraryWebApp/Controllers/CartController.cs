using DatabaseConnection.Models;
using DatabaseConnection.TableService;
using DatabaseConnection.UsersTableServices;
using firstLesson.resources;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LibraryWebApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            UserDBService userDBService = new UserDBService();
            BookDBService bookDBService = new BookDBService();
            BookDBController bookDBController = new BookDBController();

            string cart = HttpContext.Session.GetString("Cart");
            double subtotal = 0;
            double discount = 0;
            double cashPenalty = 0;

            if (!String.IsNullOrEmpty(cart))
            {
                List<int> booksId = HttpContext.Session.GetString("Cart").Split(';').Select(Int32.Parse).ToList();
                List<DatabaseConnection.Models.BookInCard> booksList = bookDBService.getBookByBookIdInIntList(booksId);
                HashSet<BorrowedBook> borrowedBooks = bookDBController.GetBorrowedBooksByBooksID(booksId);
                HashSet<BookQueue> booksInQueue = bookDBController.GetBookQueueByBooksID(booksId);

                foreach(BookInCard book in booksList)
                { 
                    book._BorrowStartDate = DateTime.Now;
                    book._BorrowEndDate = DateTime.Now.AddMonths(AppConfig.MaxBorrowTimeMonths);

                    foreach(BorrowedBook borrowedBook in borrowedBooks)
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
                        if ((bookInQueue._bookId == book._Id) &&(book._StatusId != 0))
                        {
                            book._BorrowStartDate = bookInQueue._borrowTo;
                            book._BorrowEndDate = book._BorrowStartDate.AddMonths(AppConfig.MaxBorrowTimeMonths);
                            booksInQueue.Remove(bookInQueue);
                        }
                    }

                }


                BookOfTheDay bookOfTheDay = bookDBController.GetBOTD();
                if(bookOfTheDay != null)
                {
                    booksList.Where(book => book._Id == bookOfTheDay._id).Select(y => y._PriceAfterDiscount = bookOfTheDay._priceAfterDiscount).ToList();
                }

                ViewBag.BooksInCart = booksList;
                foreach (var book in booksList)
                {
                    subtotal += book._Price;
                    if(book._PriceAfterDiscount > 0)
                        discount += book._Price-book._PriceAfterDiscount;
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

        public IActionResult RemoveFromCart(int bookToDelete)
        {
            UserDBService userDBService = new UserDBService();
            BookDBService bookDBService = new BookDBService();
            BookDBController bookDBController = new BookDBController();

            string cart = HttpContext.Session.GetString("Cart");
            int itemInCart = (int)HttpContext.Session.GetInt32("CartCount");
            if (itemInCart > 0) itemInCart = itemInCart - 1;

            double subtotal = 0;
            double discount = 0;
            double cashPenalty = 0;

            if (cart != null || cart != "")
            {

                List<int> booksId = cart.Split(';').Select(Int32.Parse).ToList();
                booksId.Remove(bookToDelete);

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


                if (booksId.Count > 0)
                {
                    foreach (var book in booksList)
                    {
                        subtotal += book._Price;
                        if (book._PriceAfterDiscount > 0)
                            discount += book._Price - book._PriceAfterDiscount;
                    }

                    cart = string.Join(";", booksId);
                }
                else
                {
                    cart = "";
                }
            }

            ViewBag.Users = userDBService.GetUsers();
            HttpContext.Session.SetString("Cart", cart);
            HttpContext.Session.SetInt32("CartCount", itemInCart);
            double total = subtotal - discount + cashPenalty;
            ViewBag.Discount = discount;
            ViewBag.CashPenalty = cashPenalty;
            ViewBag.Subtotal = subtotal;
            ViewBag.Total = total;
            return View("/Views/Cart/Index.cshtml");
        }


        public IActionResult RemoveAllFromCart()
        {
            UserDBService userDBService = new UserDBService();
            double subtotal = 0;
            double discount = 0;
            double cashPenalty = 0;


            double total = subtotal - discount + cashPenalty;
            ViewBag.Discount = discount;
            ViewBag.CashPenalty = cashPenalty;
            ViewBag.Subtotal = subtotal;
            ViewBag.Total = total;
            ViewBag.Users = userDBService.GetUsers();

            HttpContext.Session.SetInt32("CartCount", 0);
            HttpContext.Session.Remove("Cart");
            return View("/Views/Cart/Index.cshtml");
        }


        [HttpPost]
        public IActionResult Checkout(List<string> startDate, List<string> endDate, int otherUserId)
        {
            UserDBService userDBService=new UserDBService();
            BorrowDBService borrowDBService = new BorrowDBService();
            BookDBController bookDBController = new BookDBController();
            BookDBService bookDBService = new BookDBService();
            bool checkoutAvailable = true;
            double subtotal = 0;
            double discount = 0;
            double cashPenalty = 0;


            string cart = HttpContext.Session.GetString("Cart");
            int userId = (int)HttpContext.Session.GetInt32("userId");

            if (otherUserId != 0)
                userId = otherUserId;


            if(!String.IsNullOrEmpty(cart))
            {
                List<int> booksId = HttpContext.Session.GetString("Cart").Split(';').Select(Int32.Parse).ToList();
                List<DatabaseConnection.Models.BookInCard> booksList = bookDBService.getBookByBookIdInIntList(booksId);
                BookOfTheDay bookOfTheDay = bookDBController.GetBOTD();
                if (bookOfTheDay != null)
                {
                    booksList.Where(book => book._Id == bookOfTheDay._id).Select(y => y._PriceAfterDiscount = bookOfTheDay._priceAfterDiscount).ToList();
                }


                for (int i = 0; i < booksList.Count; i++)
                {
                    booksList[i]._BorrowStartDate = DateTime.Parse(startDate[i]);
                    booksList[i]._BorrowEndDate = DateTime.Parse(endDate[i]);
                    TimeSpan interval = booksList[i]._BorrowEndDate - booksList[i]._BorrowStartDate;
                }


                HashSet<BorrowedBook> borrowedBooks = bookDBController.GetBorrowedBooksByBooksID(booksId);
                HashSet<BookQueue> queueBooks = bookDBController.GetBookQueueByBooksID(booksId);
                Dictionary<int, int> booksInQueue = bookDBController.CountBookQueueByBookIDs();
                List<int> bookIsBorrowedOrInQueue = bookDBController.CheckBooksBorrowOrQueueByUserId(userId);

                HashSet<BookInCard> booksToBorrow = new HashSet<BookInCard>();
                HashSet<BookInCard> booksToQueue = new HashSet<BookInCard>();

                foreach (var book in booksList)
                {
                    BorrowedBook borrowedBook = borrowedBooks.FirstOrDefault(x => x.id == book._Id);
                    HashSet<BookQueue> _booksInQueue = new HashSet<BookQueue>();

                    foreach(BookQueue bookQueue in queueBooks)
                    {
                        if(bookQueue._bookId == book._Id)
                            _booksInQueue.Add(bookQueue);
                    }

                    if (!book.BookIsntBorrowedOrInQueueByUser(bookIsBorrowedOrInQueue))
                    {
                        checkoutAvailable = false;
                    }
                    else if(borrowedBook == null && DateService.IsEmpty<BookQueue>(_booksInQueue))
                    {
                        if (book.DateIsCorrect() && book.DataIsCurrent())
                        {
                            booksToBorrow.Add(book);
                        }
                        else checkoutAvailable = false;
                    }
                    else if(borrowedBook != null && DateService.IsEmpty<BookQueue>(_booksInQueue))
                    {
                        if (book.DateIsCorrectWithBorrowedBook(borrowedBook) && book.DateIsCorrect())
                        {
                            booksToQueue.Add(book);
                        }
                        else checkoutAvailable = false;
                    }
                    else if(borrowedBook != null && DateService.CountLowerThanMax<BookQueue>(_booksInQueue))
                    {
                        if(book.DateIsCorrect() && book.DateIsCorrectWithBorrowedBook(borrowedBook) && book.DateIsCorrectWithQueueBooks(_booksInQueue))
                        {
                            booksToQueue.Add(book);
                        }
                        else checkoutAvailable = false;
                    }
                    else if (borrowedBook == null && DateService.CountLowerThanMax<BookQueue>(_booksInQueue))
                    {
                        if(book.DateIsCorrect() && book.DateIsCorrectWithQueueBooks(_booksInQueue) && book.DataIsCurrent())
                        {
                            booksToBorrow.Add(book);
                        }
                        else checkoutAvailable = false;
                    }
                    else if (borrowedBook == null &&  DateService.HashSetIsFull<BookQueue>(_booksInQueue))
                    {
                        if(book.DateIsCorrect() && book.DataIsCurrent() && book.DateIsCorrectWithQueueBooks(_booksInQueue))
                        {
                            booksToBorrow.Add(book);
                        }
                        else checkoutAvailable = false;
                    }
                    else if (borrowedBook != null && DateService.HashSetIsFull<BookQueue>(_booksInQueue))
                    {
                        checkoutAvailable = false;
                        book.NoSpaceForBorrowOrQueue();
                    }
                }


                if (checkoutAvailable)
                {
                    foreach (BookInCard book in booksToBorrow)
                        bookDBController.BorrowBookByUser(book, userId);

                    foreach (BookInCard book in booksToQueue)
                        bookDBController.AddToQueue(book, userId);
                }
                else
                {
                    ViewBag.BooksInCart = booksList;
                    foreach (var book in booksList)
                    {
                        subtotal += book._Price;
                        if (book._PriceAfterDiscount > 0)
                            discount += book._Price - book._PriceAfterDiscount;
                    }

                    double total = subtotal - discount + cashPenalty;
                    ViewBag.Discount = discount;
                    ViewBag.CashPenalty = cashPenalty;
                    ViewBag.Subtotal = subtotal;
                    ViewBag.Total = total;
                    ViewBag.Users = userDBService.GetUsers();

                    return View("Index");
                }
            }

            HttpContext.Session.SetString("Cart", "");
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.SetInt32("CartCount", 0);
            ViewBag.BooksInCart = "";

            List<DatabaseConnection.Models.BookDetails> bookList = bookDBController.GetBooks();
            ViewBag.KeepedCount = borrowDBService.GetNumberOfNotReturnedBooks((int)HttpContext.Session.GetInt32("userId"));
            ViewBag.Books = BookService.DetailsBooksInViewBag();
            ViewBag.BOTD = bookDBController.GetBOTD();
            return View("/Views/BooksSearching/Index.cshtml");
        }
    }
}
