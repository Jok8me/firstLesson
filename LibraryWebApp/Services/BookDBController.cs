using DatabaseConnection.TableService;

namespace LibraryWebApp.Services
{
    public class BookDBController
    {
        BookDBService bookDBService;
        public BookDBController() { bookDBService = new BookDBService(); }

        public List<DatabaseConnection.Models.Book> GetBooksBorrowedByUserId(int userId)
        {
            List<DatabaseConnection.Models.Book> itemDBList = bookDBService.GetBooksBorrowedByUserId(userId);
            return itemDBList;
        }
    }
}
