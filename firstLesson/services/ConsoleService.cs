namespace firstLesson.services
{
    public class ConsoleService
    {
        public static int MultipleChoice(params string[] options)
        {


            int currentSelected = 0;
            const int startX = 2;
            const int startY = 2;
            const int boxSizeX = 10;
            const int boxSizeY = 5;
            const char horizontalSymbol = '-';
            const char verticalSymbol = '|';
            ConsoleKey key;

            do
            {
                DrawService.DrawBox(horizontalSymbol, verticalSymbol, boxSizeX,boxSizeY,startY);

                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(startX + i, startY + i);

                    if (i == currentSelected)
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write(options[i]);
                    Console.ResetColor();
                }


                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (currentSelected >= 1)
                                currentSelected -= 1;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (currentSelected + 1 < options.Length)
                                currentSelected += 1;
                            break;
                        }
                }
            } while (key != ConsoleKey.Enter);

            Console.SetCursorPosition(boxSizeY, boxSizeX);
            return currentSelected;
        }
    }
}
