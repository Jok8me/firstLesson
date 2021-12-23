using firstLesson.resources;
using firstLesson.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    internal class ManageUsersView : View
    {
        public ManageUsersView()
        {
            DrawViewBox();
            int selectedMenuOption = ConsoleService.MultipleChoice("Add user", "Search user", "Quit");
            if (selectedMenuOption == 0)
            {
                AddUserView addUserView = new AddUserView();
            }
            else if (selectedMenuOption == 1)
            {
                SearchUserView searchUserView = new SearchUserView();
            }
            else if (selectedMenuOption == 2)
            {
                Console.CursorTop = Console.WindowTop + Console.WindowHeight - 10;
                Environment.Exit(0);
            }
            else
            {
                throw new Exception(StringResources.WRONG_OPTION_SELECTED);
            }
        }

    }
}