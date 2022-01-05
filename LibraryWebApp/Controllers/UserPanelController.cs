using DatabaseConnection.UsersTableServices;
using LibraryWebApp.Models;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    public class UserPanelController : Controller
    {
        public IActionResult Index()
        {
            UserDBController userDBController = new UserDBController();
            string login = HttpContext.Session.GetString("Login");
            string password = HttpContext.Session.GetString("Password");
            User user = userDBController.GetUserByLoginAndPassword(login, password); ;

            HttpContext.Session.SetInt32("id", user.id);

            ViewBag.login = user.login;
            ViewBag.password = user.password;
            return View();
        }

        public IActionResult UpdateProfil(string login, string password)
        {
            UserDBController userDBController = new UserDBController();
            int id = (int)HttpContext.Session.GetInt32("id");
            userDBController.UpdateUserSearchedById(id, login, password);

            HttpContext.Session.SetString("Login", login);
            HttpContext.Session.SetString("Password", password);
            ViewBag.login = login;
            ViewBag.password = password;

            return View("Views/UserPanel/Index.cshtml");
        }

    }
}
