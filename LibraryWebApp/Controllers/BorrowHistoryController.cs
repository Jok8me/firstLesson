using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    public class BorrowHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
