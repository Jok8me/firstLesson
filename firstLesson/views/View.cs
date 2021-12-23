using firstLesson.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    public abstract class View
    {
        protected int sizeX = 25;
        int sizeY = 8;

        public void DrawViewBox()
        {
            Console.CursorVisible = false;
            DrawService.DrawBox(sizeX, sizeY);
        }
    }
}
