using DatabaseConnection;
using firstLesson.models.Users;
using firstLesson.resources;
using firstLesson.services;
using firstLesson.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    public class AddUserView : View
    {
        public AddUserView(MainWindow mainWindow) : base(mainWindow)
        {
        }

        public override void Run()
        {

            _mainWindow._consoleRectangle.Draw();
            Console.CursorVisible = true;
            Console.SetCursorPosition(1, 1);
            string message = "Add user:";

            IDictionary<string, string> inputOptions = InputService.InputOptions(message);

            if (!String.IsNullOrEmpty(inputOptions["Login"]) && !String.IsNullOrEmpty(inputOptions["Password"]))
            {
                userDBService.insertUser(new DatabaseConnection.Models.User(inputOptions["Login"], inputOptions["Password"], 1));
                string[] options = { "Ok" };
                _mainWindow._messageView.Run("User " + inputOptions["Login"] + " successfully added.", options);
            }
            else
            {
                throw new Exception(StringResources.WRONG_INPUT);
            }
        }

    }
}
