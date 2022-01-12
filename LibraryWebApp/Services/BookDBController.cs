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

        public List<DatabaseConnection.Models.BookDetails> GetBooksByTypeAndCategory(int bookType, List<int> bookCategory)
        {
            return bookDBService.GetBooksByTypeAndCategory(bookType, bookCategory);
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


        public void returnBookByBookIdAndUserId(int bookId, int userId)
        {
            borrowDBService.returnBookByBookIdAndUserId(bookId, userId);
        }
    }
}
