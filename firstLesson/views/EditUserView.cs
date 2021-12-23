using firstLesson.models;
using firstLesson.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    public class EditUserView : View
    {
        public EditUserView(User user)
        {
            DrawViewBox();
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("Znaleziono usera:" + user.getLogin());
            int selectedMenuOption = ConsoleService.MultipleChoice("Edit", "Delete");
        }
    }
}
