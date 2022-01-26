using DatabaseConnection.Models;
using DatabaseConnection.Models.DiscountStrategies;

namespace LibraryWebApp.Services
{
    public static class BookService
    {
        public static List<DatabaseConnection.Models.BookDetails> DetailsBooksInViewBag()
        {
            BookDBController bookDBController = new BookDBController();
            List<DatabaseConnection.Models.BookDetails> bookList = bookDBController.GetBooks();
            BookOfTheDay bookOfTheDay = bookDBController.GetBOTD();



            if (bookOfTheDay != null)
            {
                bookList.Where(book => book.id == bookOfTheDay._id).Select(y => y.priceAfterDiscount = bookOfTheDay._priceAfterDiscount).ToList();
            }

            return bookList;
        }
    }
}
