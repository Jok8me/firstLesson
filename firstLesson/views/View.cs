using DatabaseConnection.TableService;
using DatabaseConnection.UsersTableServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.Views
{
    public class View
    {
        protected MainWindow _mainWindow;
        protected UserDBService userDBService;
        protected BookDBService itemDBService;

        public View(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            userDBService = new UserDBService();
            itemDBService = new BookDBService();
        }
        virtual public void Run()
        {

        }
    }
}
