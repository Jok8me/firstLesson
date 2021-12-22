using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.services
{
    public class InputService
    {
        private static IDictionary<string, string> inputOptions = new Dictionary<string, string>(){
                {"Login", "unknow"},
                {"Password", "unknow"}
                };

        public static IDictionary<string, string> InputOptions()
        {
            DrawService.DrawBox(inputOptions.Count, 5);

            for (int i = 0; i < inputOptions.Count; i++)
            {
                Console.SetCursorPosition(2, 2 + i);
                Console.Write(inputOptions.ElementAt(i).Key + ": ");
                inputOptions[inputOptions.ElementAt(i).Key] = Console.ReadLine();
            }

            return inputOptions;
        }


        public static IDictionary<string, string> InputOptions(int ammoutOfData)
        {
            DrawService.DrawBox(ammoutOfData, 5);

            for (int i = 0; i < ammoutOfData; i++)
            {
                Console.SetCursorPosition(2, 2 + i);
                Console.Write(inputOptions.ElementAt(i).Key + ": ");
                inputOptions[inputOptions.ElementAt(i).Key] = Console.ReadLine();
            }

            return inputOptions;
        }

    }

}
