using DatabaseConnection;
using DatabaseConnection.Models;
using DatabaseConnection.TableService;
using firstLesson.services;

namespace firstLesson.Views.BookViews
{
    public class AddItemView : View
    {
        public AddItemView(MainWindow mainWindow) : base(mainWindow)
        {
        }

        public override void Run()
        {
            _mainWindow._consoleRectangle.Draw();
            Console.CursorVisible = true;
            Console.SetCursorPosition(1, 1);
            string message = "Add book:";

            BookDBService itemDBService = new BookDBService();

            IDictionary<string, string> itemDictionary = InputService.InputItemOptions(message);
            Book bookToAdd = new Book(
                itemDictionary.ElementAt(0).Value,
                DateTime.Parse(itemDictionary.ElementAt(2).Value),
                Convert.ToInt32(itemDictionary.ElementAt(4).Value),
                Convert.ToInt32(itemDictionary.ElementAt(5).Value),
                Convert.ToDouble(itemDictionary.ElementAt(1).Value));
            itemDBService.inserBook(bookToAdd);
        }
    }
  
}
