using firstLesson.models;
using firstLesson.services;

namespace firstLesson.views
{
    public class LoginView
    {
        public static void Run()
        {
            HashSet<User> users = new HashSet<User>();
            users.Add(new User("user", "user"));
            users.Add(new User("admin", "admin", enums.Role.Admin));


            string login, password;
            User user;

            Console.Clear();
               Console.Write("Login: ");
               login = Console.ReadLine();
                Console.Write("Password:");
                password = Console.ReadLine();
            

            if ( String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
            {
                throw new Exception("cannot find user with empty input");
            }
            else user = new User(login, password);

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
