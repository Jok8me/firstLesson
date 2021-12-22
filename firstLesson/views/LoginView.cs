using firstLesson.models;
using firstLesson.resources;
using firstLesson.services;

namespace firstLesson.views
{
    public class LoginView
    {
        public static void Run()
        {        
            User user;
            IDictionary<string, string> inputOptions = InputService.InputOptions();


            if (String.IsNullOrEmpty(inputOptions["Login"]) || String.IsNullOrEmpty(inputOptions["Password"]))
            {
                throw new Exception(StringResources.CANNOT_FIND_USER);
            }
            else user = new StandardUser(inputOptions["Login"], inputOptions["Password"]);


            DrawService.DrawBox(3, 20);
            Console.SetCursorPosition(2, 2);

            if (LoginService.LoginAuthentication(User.users, user))
            {
                AdminPanelVeiw adminPanelVeiw = new AdminPanelVeiw();
            }
            else
            {
                Console.WriteLine("Wrong password or no permission");
            }
        }
    }
}
