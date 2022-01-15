using firstLesson.resources;
using firstLesson.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.Views
{
    public class AppConfigurationView : View
    {
        public AppConfigurationView(MainWindow mainWindow) : base(mainWindow)
        {
        }

        public override void Run()
        {

            _mainWindow._consoleRectangle.Draw();
            Console.CursorVisible = true;
            Console.SetCursorPosition(1, 1);
            string message = "App configuration :";

            IDictionary<string, string> inputConfigurationOptions = InputService.InputConfigurationOptions(message);

            if (!String.IsNullOrEmpty(inputConfigurationOptions["Max borrow time(Months)"]))
            {
                AppConfig.MaxBorrowTimeMonths = Int32.Parse(inputConfigurationOptions["Max borrow time(Months)"]);
                string[] options = { "Ok" };
                _mainWindow._messageView.Run("Max borrow time update to " + inputConfigurationOptions["Max borrow time(Months)"], options);
            }
            else
            {
                throw new Exception(StringResources.WRONG_INPUT);
            }
        }

    }
}

