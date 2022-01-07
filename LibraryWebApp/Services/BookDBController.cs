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

        public List<DatabaseConnection.Models.BorrowedBook> GetBooksBorrowedByUserId(int userId)
        {
            List<DatabaseConnection.Models.BorrowedBook> itemDBList = bookDBService.GetBooksBorrowedByUserId(userId);
            return itemDBList;
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

    }
}
