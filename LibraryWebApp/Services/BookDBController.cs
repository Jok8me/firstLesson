using DatabaseConnection.Models;
using DatabaseConnection.TableService;

namespace LibraryWebApp.Services
{
    public class BookDBController
    {
        BookDBService bookDBService;
        BorrowDBService borrowDBService;
        public BookDBController() { 
            bookDBService = new BookDBService(); 
            borrowDBService = new BorrowDBService();
        }

        public List<DatabaseConnection.Models.BookDetails> GetBooksByTypeAndCategoryAndSearchInput(int bookType, List<int> bookCategory, string searchInput)
        {
            return bookDBService.GetBooksByTypeAndCategoryAndSearchInput(bookType, bookCategory, searchInput);
        }

        public List<DatabaseConnection.Models.BorrowedBook> GetBooksCurrentBorrowedByUserId(int userId)
        {
            List<DatabaseConnection.Models.BorrowedBook> itemDBList = bookDBService.GetBooksCurrentBorrowedByUserId(userId);
            return itemDBList;
        }

        internal void ReturnBookById(int bookId)
        {
            bookDBService.ReturnBookById(bookId);
        }

        public DatabaseConnection.Models.BookDetails GetBookByBookId(int bookId)
        {
            return bookDBService.GetBookByBookId(bookId);
        }

        public List<DatabaseConnection.Models.BookDetails> GetBooks()
        {
            return bookDBService.GetBooks();
        }

        public void BorrowBookByUser(int bookId, int userId)
        {
            borrowDBService.borrowBook(bookId, userId);
        }


        public void ReturnBookByBookIdAndUserId(int bookId, int userId)
        {
            borrowDBService.ReturnBookByBookIdAndUserId(bookId, userId);
        }

        public HashSet<BorrowedBook> GetBorrowedBookByBooksID(List<int> booksID)
        {
            return borrowDBService.GetBorrowedBookByBooksID(booksID);
        }

        public void BookBookByUser(int bookId, int userId, DateTime bookFrom, DateTime bookTo)
        {
            bookDBService.BookBookByUser(bookId, userId, bookFrom, bookTo);
        }
    }
}
