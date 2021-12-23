using firstLesson.models;
using firstLesson.resources;
using firstLesson.services;

namespace firstLesson.views
{
    public class LoginView : View
    {
        public LoginView()
        {        
            User user;
            DrawViewBox();

            Console.CursorVisible = true;
            IDictionary<string, string> inputOptions = InputService.InputOptions();

            if (String.IsNullOrEmpty(inputOptions["Login"]) || String.IsNullOrEmpty(inputOptions["Password"]))
            {
                throw new Exception(StringResources.CANNOT_FIND_USER);
            }
            else user = new StandardUser(inputOptions["Login"], inputOptions["Password"]);


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
