using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.services.ConsoleServices
{
    public static class ConsoleUtils
    {
        public static void WaitForKeyPress()
        {
            Console.WriteLine("(Press any key to continue)");
            Console.ReadKey(true);
        }

        public static void QuitConsole()
        {
            Console.SetCursorPosition(0, 15);
            Environment.Exit(0);
        }
    }
}
