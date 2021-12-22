using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.services
{
    public class DrawService
    {
        public static char horizontalSymbol = '-';
        public static char verticalSymbol = '|';

        public static void DrawHorizontal(char symbol, int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write(symbol);
            }
        }

        public static void DrawVertical(char symbol, int depth, int width)
        {
            Console.WriteLine();
            for (int i = 0; i < depth; i++)
            {
                Console.Write(symbol);
                for (int j = 0; j < width; j++)
                    Console.Write(' ');
                Console.Write(symbol);
                if (i + 1 < depth)
                    Console.WriteLine();
            }
        }

        public static void DrawBox(int sizeX, int sizeY)
        {
            Console.Clear();
            Console.Write(" ");
            DrawService.DrawHorizontal(horizontalSymbol, sizeX);
            DrawService.DrawVertical(verticalSymbol, sizeY, sizeX);
            Console.Write("\n ");
            DrawService.DrawHorizontal(horizontalSymbol, sizeX);
        }
    }
}
