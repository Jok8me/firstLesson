using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.services
{
    public class DrawService
    {
        public static void DrawHorizontal(char symbol, int length)
        {
            for(int i = 0; i< length; i++)
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

        public static void DrawBox(char horizontalSymbol, char verticalSymbol, int boxSizeX, int boxSizeY, int startY)
        {
            Console.Clear();
            DrawService.DrawHorizontal(horizontalSymbol, boxSizeX);
            DrawService.DrawVertical(verticalSymbol, boxSizeY, boxSizeX - startY);
            Console.WriteLine();
            DrawService.DrawHorizontal(horizontalSymbol, boxSizeX);
        }
    }
}
