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
    public class MessageView : View
    {        public MessageView(MainWindow mainWindow) : base(mainWindow)
        {
        }

        public void Run(string _message, string[] _prompt)
        {
            int selectedMenuOption = new ChooseOptionServices(_message, _prompt).Run();

            if (selectedMenuOption == 0 || selectedMenuOption == 1)
            {
                _mainWindow._manageUsersView.Run();
            }
            else throw new Exception(StringResources.WRONG_OPTION_SELECTED);
        }
    }
}
