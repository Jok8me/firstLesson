using firstLesson.models.Users;
namespace firstLesson.services
{
    public class LoginService
    {
        public static bool LoginAuthentication(HashSet<User> users, User loginAttemptUser)
        {
            foreach (var user in users)
            {
                if (loginAttemptUser.Equals(user) && user.getRole() == enums.Role.Admin)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
