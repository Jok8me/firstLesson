using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.services.ConsoleServices
{
    public class ChooseOptionServices
    {
        private int _selectedIndex;
        private string[] _options;
        private string _prompt;

        public ChooseOptionServices(string prompt, string[] options)
        {
            _options = options;
            _prompt = prompt;
            _selectedIndex = 0;
        }

        private void DisplayOptions()
        {

            Console.ResetColor();
            Console.WriteLine(_prompt);

            for (int i = 0; i < _options.Length; i++)
            {
                string currentOption = _options[i];
                if (i == _selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ResetColor();
                }
                SetCursorPosition.MoveCursorToCenter();
                Console.WriteLine(currentOption);


            }
        }

        public int Run()
        {
            ConsoleRectangle consoleRectangle = new ConsoleRectangle(35, 10, new System.Drawing.Point(0, 0), ConsoleColor.White);
            ConsoleKey key;

            do
            {
                consoleRectangle.Draw();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;

                if (key.Equals(ConsoleKey.UpArrow) && _selectedIndex != 0)
                    _selectedIndex--;
                else if (key.Equals(ConsoleKey.DownArrow) && _selectedIndex < _options.Length)
                    _selectedIndex++;


            } while (!key.Equals(ConsoleKey.Enter));

            Console.ResetColor();
            return _selectedIndex;
        }
    }
}
