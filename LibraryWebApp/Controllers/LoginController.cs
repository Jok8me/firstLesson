﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LibraryWebApp.Services;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Session;
using DatabaseConnection.TableService;

namespace LibraryWebApp.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData.Clear();
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        [Route("BooksSearching/Index")]
        public ActionResult Login(string login, string password)
        {
            BorrowDBService borrowDBService = new BorrowDBService();
            UserDBController userDBController = new UserDBController();
            User user = userDBController.GetUserByLoginAndPassword(login, password);


            if (!(user.login.Equals("")))
            {
                HttpContext.Session.SetInt32("userId", user.id);
                HttpContext.Session.SetString("Login", user.login);
                HttpContext.Session.SetString("Password", user.password);
                HttpContext.Session.SetInt32("CartCount", 0);

                BookDBController bookDBController = new BookDBController();
                List<DatabaseConnection.Models.BookDetails> bookList = bookDBController.GetBooks();
                ViewBag.Books = bookList;
                ViewBag.KeepedCount = borrowDBService.GetNumberOfNotReturnedBooks((int)HttpContext.Session.GetInt32("userId"));
                return View("/Views/BooksSearching/Index.cshtml");
            }
            else
            {
                ViewBag.login = "";
                ViewBag.error = "Invalid Account";
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.SetInt32("CartCount", 0);
            ViewBag.login = "";
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


    }
}
