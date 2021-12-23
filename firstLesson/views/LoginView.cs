using firstLesson.models.Users;
using firstLesson.resources;
using firstLesson.services;

namespace firstLesson.views
{
    public class LoginView : View
    {
        private static int loginAttempt = 3;
        public LoginView()
        {        
            User user;
            DrawViewBox();

            Console.CursorVisible = true;
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("Login attempt: " + loginAttempt);

            IDictionary<string, string> inputOptions = InputService.InputOptions();

            if (String.IsNullOrEmpty(inputOptions["Login"]) || String.IsNullOrEmpty(inputOptions["Password"]))
            {
                throw new Exception(StringResources.CANNOT_FIND_USER);
            }
            else user = new StandardUser(inputOptions["Login"], inputOptions["Password"]);


            if (LoginService.LoginAuthentication(User.users, user))
            {
                loginAttempt = 3;
                AdminPanelVeiw adminPanelVeiw = new AdminPanelVeiw();
            }
            else
            {
                loginAttempt--;
                if (loginAttempt > 0)
                {
                    LoginView veiw = new LoginView();
                } else
                {
                    Console.CursorTop = Console.WindowTop + Console.WindowHeight - 10;
                    Environment.Exit(0);
                }
            }
        }
    }
}
