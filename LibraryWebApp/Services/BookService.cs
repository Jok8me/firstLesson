namespace LibraryWebApp.Services
{
    public class BookService
    {
        public bool isBorrowed<T>(ref T obj)
        {
            if (obj != null)
                return true;
            return false;
        }
    }
}
