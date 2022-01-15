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
    public class AdminPanelVeiw : View
    {
        HashSet<string> adminPanelOptions = new HashSet<string> {"Manage Users" , "Manage Books", "App configuration","Logout" };
        private string prompt = "Admin panel";

        public AdminPanelVeiw(MainWindow mainWindow) : base(mainWindow)
        {
        }

        public override void Run()
        {
            ChooseOptionServices chooseOptionServices = new ChooseOptionServices(prompt, adminPanelOptions.ToArray());
            int selectedMenuOption = chooseOptionServices.Run();

            if (selectedMenuOption == 0)
            {
                _mainWindow._manageUsersView.Run();

            }
            else if (selectedMenuOption == 1)
            {
                _mainWindow._manageBooksView.Run();
            }
            else if (selectedMenuOption == 2)
            {
                _mainWindow._appConfigurationView.Run();
            }
            else if (selectedMenuOption == 3)
            {
                _mainWindow._loginView.Run();
            }
            else
            {

            }
        }
    }
    
}
