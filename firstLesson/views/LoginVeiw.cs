using firstLesson.models.Users;
using firstLesson.services;

namespace firstLesson.Views
{
    public class LoginVeiw : View
    {

        private static int loginAttempt = 3;

        public LoginVeiw(MainWindow mainWindow) : base(mainWindow)
        {
        }

        public override void Run()
        {
            DatabaseConnection.Models.User user;
            _mainWindow._consoleRectangle.Draw();
            Console.CursorVisible = true;
            Console.SetCursorPosition(1, 1);
            string message = "Login attempt: " + loginAttempt;

            IDictionary<string, string> inputOptions = InputService.InputOptions(message);

            if (String.IsNullOrEmpty(inputOptions["Login"]) || String.IsNullOrEmpty(inputOptions["Password"]))
            {
                throw new Exception(resources.StringResources.CANNOT_FIND_USER);
            }
            else user = new DatabaseConnection.Models.User(inputOptions["Login"], inputOptions["Password"], 0);


            if (LoginService.LoginAuthentication(user))
            {
                loginAttempt = 3;
                _mainWindow._adminPanelView.Run();
            }
            else
            {
                loginAttempt--;
                if (loginAttempt > 0)
                {
                    _mainWindow._loginView.Run();
                }
                else
                {
                    Console.Clear();
                }
            }
        }
    }
}