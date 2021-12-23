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
            int selectedMenuOption = ConsoleService.MultipleChoice("Add user", "Search user", "Back");
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
                AdminPanelVeiw adminPanelVeiw = new AdminPanelVeiw();
            }
            else
            {
                throw new Exception(StringResources.WRONG_OPTION_SELECTED);
            }
        }

    }
}