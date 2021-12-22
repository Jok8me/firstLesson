using firstLesson.resources;
using firstLesson.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    internal class ManageUsersView
    {
        HashSet<string> manageUsersOptions = new HashSet<string> { "Add user", "Search user" };

        public ManageUsersView()
        {
            int selectedMenuOption = ConsoleService.MultipleChoice(manageUsersOptions.ToArray());
            if (selectedMenuOption == 0)
            {
                AddUserView addUserView = new AddUserView();
            }
            else if (selectedMenuOption == 1)
            {
                SearchUserView searchUserView = new SearchUserView();
            }
            else
            {
                throw new Exception(StringResources.WRONG_OPTION_SELECTED);
            }
        }

    }
}