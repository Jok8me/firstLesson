using firstLesson.resources;
using firstLesson.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    public class MessageView
    {
        public MessageView(string message)
        {
            int selectedMenuOption = ConsoleService.MultipleChoice(message, "OK");
            if (selectedMenuOption == 0 || selectedMenuOption == 1)
            {
                ManageUsersView manageUsersView = new ManageUsersView();
            }
            else throw new Exception(StringResources.WRONG_OPTION_SELECTED);
        }
    }
}
