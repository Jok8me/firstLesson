using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    public class BooksSearchingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
