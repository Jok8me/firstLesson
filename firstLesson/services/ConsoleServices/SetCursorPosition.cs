using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.services.ConsoleServices
{
    public static class SetCursorPosition
    {
        public static void MoveCursorToCenter()
        {
            Console.SetCursorPosition(Console.GetCursorPosition().Left + 2, Console.GetCursorPosition().Top);
        }
    }
}
