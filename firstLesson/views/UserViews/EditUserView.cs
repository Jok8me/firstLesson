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
    public class EditUserView : View
    {
        public EditUserView(MainWindow mainWindow) : base(mainWindow)
        {
        }

        public void Run(int userId)
        {
            string message = "Edit user:";
            _mainWindow._consoleRectangle.Draw();
            Console.CursorVisible = true;
            Console.SetCursorPosition(1, 1);

            IDictionary<string, string> inputOptions = InputService.InputOptions(message);


            if (!String.IsNullOrEmpty(inputOptions["Login"]) && !String.IsNullOrEmpty(inputOptions["Password"]))
            {
                userDBService.updateUserById(userId, inputOptions["Login"], inputOptions["Password"]);

                string _message = inputOptions["Login"] + " correctly updated";
                string[] options = { "Ok" };
                _mainWindow._messageView.Run(_message, options);
            }
            else
            {
                throw new Exception(StringResources.WRONG_INPUT);
            }
        }
    }
}
