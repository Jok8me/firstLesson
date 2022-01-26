using DatabaseConnection.Models;
namespace LibraryWebApp.Services
{
    public static class SearchService
    {
        public static List<BookDetails> Search(List<BookDetails> bookList, int bookType, List<int> bookCategory, string searchInput)
        {
            List<BookDetails> result = new List<BookDetails>();

            if (!bookCategory.Contains(0) && bookCategory.Count != 0)
            {
                foreach (int category in bookCategory)
                {
                    result.AddRange(bookList.Where(x => x.categoryId == category).ToList());
                }
            }
            else result = bookList;

            if(bookType != 0)
            {
                result = result.Where(x => x.typeId == bookType-1).ToList();
            }

            if(searchInput != "")
            {
                result = result.Where(x => x.title.Contains(searchInput) ||
                                           x.name.Contains(searchInput) ||
                                           x.surname.Contains(searchInput)).ToList();
            }
            return result;
        }
    }
}
