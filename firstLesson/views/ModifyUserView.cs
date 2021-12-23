using firstLesson.models;
using firstLesson.services;
using firstLesson.resources;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    public class ModifyUserView : View
    {
        public ModifyUserView(int indexOfUser)
        {
            DrawViewBox();
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("Znaleziono usera:" + User.users.ElementAt(indexOfUser).getLogin());
            int selectedMenuOption = ConsoleService.MultipleChoice("Edit", "Delete", "Back");

            if (selectedMenuOption == 0)
            {
                EditUserView editUserView = new EditUserView(indexOfUser);
            }
            else if (selectedMenuOption == 1)
            {
                string loginUserToRemove = User.users.ElementAt(indexOfUser).getLogin();
                User.users.Remove(User.users.ElementAt(indexOfUser));
                MessageView messageView = new MessageView("User " + loginUserToRemove + " sucesfully removed.");
            }
            else if (selectedMenuOption == 2)
            {
                AdminPanelVeiw adminPanelVeiw = new AdminPanelVeiw();
            }
            else
            {

            }
        }
    }
}
