using firstLesson.resources;
using firstLesson.services.ConsoleServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.Views
{
    public class ManageBooksView : View
    {
        public ManageBooksView(MainWindow mainWindow) : base(mainWindow)
        {
        }

        public override void Run()
        {
            string prompt = "Manage Book";
            string[] options = { "Add book", "Search book", "Back" };
            int selectedMenuOption = new ChooseOptionServices(prompt, options).Run();

            if (selectedMenuOption == 0)
            {
                _mainWindow._addBookView.Run();
            }
            else if (selectedMenuOption == 1)
            {
                _mainWindow._searchBookView.Run();
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
