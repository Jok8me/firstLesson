using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
