
using DatabaseConnection.UsersTableServices;

namespace firstLesson.services
{
    public class LoginService
    {
        public static bool LoginAuthentication(DatabaseConnection.Models.User userToFind)
        {
            UserDBService userDBService = new UserDBService();
            DatabaseConnection.Models.User user = userDBService.searchAndGetUserInDB(userToFind);

           if (!(user is null) && user.login.Equals(userToFind.login) && user.password.Equals(userToFind.password) && user.role == 0)
            return true;

            return false;
        }
    }
}
