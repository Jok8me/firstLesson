using firstLesson.enums;
using firstLesson.models.Books;
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

        private static IDictionary<string, string> inputItemOptions = new Dictionary<string, string>(){
            {"Title", "unknow"},
            {"Price", "0,0"},
            {"PublicationDate", "Jan 1, 2000" },
            {"Status", "0" },
            {"Type", "0" }
            };

        public static IDictionary<string, string> InputOptions(string message)
        {
                Console.WriteLine(message);
            for (int i = 0; i < inputOptions.Count; i++)
            {
                Console.SetCursorPosition(2, 3 + i);
                Console.Write(inputOptions.ElementAt(i).Key + ": ");
                inputOptions[inputOptions.ElementAt(i).Key] = Console.ReadLine();
            }

            return inputOptions;
        }


        public static IDictionary<string, string> InputOptions(int ammoutOfData, string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < ammoutOfData; i++)
            {
                Console.SetCursorPosition(2, 2 + i);
                Console.Write(inputOptions.ElementAt(i).Key + ": ");
                inputOptions[inputOptions.ElementAt(i).Key] = Console.ReadLine();
            }
            return inputOptions;
        }

        public static IDictionary<string, string> InputItemOptions(string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < inputItemOptions.Count; i++)
            {
                Console.SetCursorPosition(2, 3 + i);
                Console.Write(inputItemOptions.ElementAt(i).Key + ": ");
                inputItemOptions[inputItemOptions.ElementAt(i).Key] = Console.ReadLine();
            }

            return inputItemOptions;
        }

    }

}
