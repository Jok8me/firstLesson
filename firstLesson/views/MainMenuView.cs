using firstLesson.resources;
using firstLesson.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    public class MainMenuView : View
    {
        public MainMenuView()
        {
            DrawViewBox();

            int selectedMenuOption = ConsoleService.MultipleChoice("Login", "Quit");
            if (selectedMenuOption == 0)
            {
                LoginView loginView = new LoginView();
            }
            else if (selectedMenuOption == 1)
            {
                Environment.Exit(0);
            }
            else
            {
                throw new Exception(StringResources.WRONG_OPTION_SELECTED);
            }

        }

    }
}
