using firstLesson.models;
using firstLesson.services;
using firstLesson.resources;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using firstLesson.models.Users;
using firstLesson.Views;
using firstLesson.services.ConsoleServices;
using DatabaseConnection;

namespace firstLesson.views
{
    public class ModifyUserView : View
    {
        public ModifyUserView(MainWindow mainWindow) : base(mainWindow)
        {
        }


        public void Run(DatabaseConnection.Models.User user)
        {

            string prompt = "Znaleziono usera:" + user.login;
            string[] options = { "Edit", "Delete", "Back" };
            int selectedMenuOption = new ChooseOptionServices(prompt, options).Run();

            switch (selectedMenuOption)
            {
                case 0:
                    _mainWindow._editUserView.Run(user.Id);
                    break;
                case 1:
                    string[] _options = { "Ok" };
                    string message;

                    userDBService.deleteUserById(user.Id);
                    message = "User " + user.login + " sucesfully removed.";
                    _mainWindow._messageView.Run(message, _options);

                    break;
                case 2:
                    _mainWindow._adminPanelView.Run();
                    break;
                default:
                    _mainWindow._modifyUserView.Run();
                    break;

            }
        }
    }
}
