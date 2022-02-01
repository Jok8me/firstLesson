using firstLesson.resources;
using firstLesson.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.Views.BookViews
{
    public class SearchBookView : View
    {
        public SearchBookView(MainWindow mainWindow) : base(mainWindow)
        {
        }

        public override void Run()
        {
            _mainWindow._consoleRectangle.Draw();
            Console.CursorVisible = true;
            Console.SetCursorPosition(1, 1);
            string message = "Search book:";

            IDictionary<string, string> inputOptions = InputService.InputOptions(1, message);

            if (!String.IsNullOrEmpty(inputOptions["Login"]))
            {
                DatabaseConnection.Models.User user = userDBService.searchUserByLogin(inputOptions["Login"]);

                if (user.login.Equals(inputOptions["Login"]))
                    _mainWindow._modifyUserView.Run(user);

                string messageCantFindUser = "Can't find user '" + inputOptions["Login"];
                string[] options = { "Ok" };
                _mainWindow._messageView.Run(messageCantFindUser, options);
            }
            else
            {
                throw new Exception(StringResources.WRONG_INPUT);
            }
        }
    }
}
