using firstLesson.resources;
using firstLesson.services;
using firstLesson.services.ConsoleServices;
using firstLesson.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    public class ManageUsersView : View
    {
        public ManageUsersView(MainWindow mainWindow) : base(mainWindow)
        {
        }

        public override void Run()
        {
            string prompt = "ManageUserView";
            string[] options = { "Add user", "Search user", "Back" };
            int selectedMenuOption = new ChooseOptionServices(prompt, options).Run();

            if (selectedMenuOption == 0)
            {
                _mainWindow._addUserView.Run();
            }
            else if (selectedMenuOption == 1)
            {
                _mainWindow._searchUserView.Run();
            }
            else if (selectedMenuOption == 2)
            {
                _mainWindow._adminPanelView.Run();
            }
            else
            {
                throw new Exception(StringResources.WRONG_OPTION_SELECTED);
            }
        }

    }
}