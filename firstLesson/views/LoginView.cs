using firstLesson.models;
using firstLesson.services;

namespace firstLesson.views
{
    public class LoginView
    {
        public static void Run()
        {
            HashSet<User> users = new HashSet<User>();
            users.Add(new Admin("admin", "admin", enums.Role.Admin));


            string login, password;
            User user;
          

            DrawService.DrawBox('-', '|', 20, 5, 2);
            Console.SetCursorPosition(3, 2);

            Console.Write("Login: ");
            login = Console.ReadLine();
            Console.SetCursorPosition(3, 3);
            Console.Write("Password:");
            password = Console.ReadLine();
            

            if ( String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
            {
                throw new Exception("cannot find user with empty input");
            }
            else user = new StandardUser(login, password);

            Console.Clear();
            if (LoginService.LoginAuthentication(users, user))
            {
                Console.WriteLine("Logged");
            }
            else
            {
                Console.WriteLine("Wrong password");
            }
        }
    }
}
