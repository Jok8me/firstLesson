using firstLesson.services.ConsoleServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ConsoleColor;

namespace firstLesson.Views
{
    public class MainMenuView : View
    {
        string _prompt = "Library aplication";
        string[] _options = {"Login", "Quit"}; 
        int choosenOption = 0;

        public MainMenuView(MainWindow mainWindow) : base(mainWindow)
        {
        }



        public override void Run()
        {
            ChooseOptionServices chooseOptionServices = new ChooseOptionServices(_prompt, _options);
            choosenOption = chooseOptionServices.Run();

            switch (choosenOption)
            {
                case 0:
                    _mainWindow._loginView.Run();
                    break;  
                case 1:
                    ConsoleUtils.QuitConsole();
                    break;

            }

            ConsoleUtils.WaitForKeyPress();
        }
    }
}
