using firstLesson.models;
namespace firstLesson.services
{
    public class LoginService
    {
        public static bool LoginAuthentication(HashSet<User> users, User loginAttemptUser)
        {
            if (users.Contains(loginAttemptUser))
                return true;
            return false;

        }
    }
}
