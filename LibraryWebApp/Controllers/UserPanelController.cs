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
            ViewBag.name = user.name;
            ViewBag.surname = user.surname;
            ViewBag.email = user.email;
            ViewBag.phoneNumber = user.phoneNumber;
            return View();
        }

        public IActionResult UpdateProfil(string login, string password, string name, string surname, string email, string phoneNumber)
        {
            UserDBController userDBController = new UserDBController();
            int id = (int)HttpContext.Session.GetInt32("id");
            userDBController.UpdateUserSearchedById(id, login, password, name, surname, email, phoneNumber);

            HttpContext.Session.SetString("Login", login);
            HttpContext.Session.SetString("Password", password);
            HttpContext.Session.SetString("Name", name);
            HttpContext.Session.SetString("Surname", surname);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("PhoneNumber", phoneNumber);

            ViewBag.login = login;
            ViewBag.password = password;
            ViewBag.name = name;
            ViewBag.surname = surname;
            ViewBag.email = email;
            ViewBag.phoneNumber = phoneNumber;


            return View("Views/UserPanel/Index.cshtml");
        }

    }
}
