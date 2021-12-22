namespace firstLesson.services
{
    public class ConsoleService
    {
        public static int MultipleChoice(params string[] options)
        {
            int startTextX = options.Max().Length;
            int startTextY = 2;
            int currentSelected = 0;
            ConsoleKey key;

            do
            {
                DrawService.DrawBox(options.Count(), options.Max().Length*2);
                for (int i = 0; i < options.Length; i++)
                {
                   Console.SetCursorPosition(startTextX + i, startTextY + i);

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
            return currentSelected;
        }
    }
}
