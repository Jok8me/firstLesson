using firstLesson.models.Users;
using firstLesson.enums;
using firstLesson.views;
using firstLesson.models.Books;
using firstLesson.models.Books.DiscountStrategies;
using firstLesson.services.DiscountServices;
using firstLesson.services.BookServices;
using firstLesson.Views;
using firstLesson.services.ConsoleServices;
using DatabaseConnection;
using DatabaseConnection.UsersTableServices;

UserDBService userDBService = new UserDBService();

string Title = "Library";
Console.CursorVisible = false;
MainWindow mainWindow = new MainWindow();

mainWindow.Start();
