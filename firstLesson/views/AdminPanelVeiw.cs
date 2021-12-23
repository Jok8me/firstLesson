using firstLesson.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    public class AdminPanelVeiw : View
    {
        HashSet<string> adminPanelOptions = new HashSet<string> {"Manage Users", "Logout" };

        public AdminPanelVeiw()
        {
            DrawViewBox();
            int selectedMenuOption = ConsoleService.MultipleChoice(adminPanelOptions.ToArray());
            if(selectedMenuOption == 0)
            {
                ManageUsersView manageUsersView = new ManageUsersView();

            } else if(selectedMenuOption == 1)
            {
                LoginView loginView = new LoginView();   
            }
            else
            {

            }
        }

    }
}
