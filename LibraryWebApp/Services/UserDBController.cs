using DatabaseConnection.UsersTableServices;
using LibraryWebApp.Models;

namespace LibraryWebApp.Services
{
    public class UserDBController
    {
        UserDBService userDBService ;
        public UserDBController() { userDBService = new UserDBService(); }

        public Models.User GetUserByLoginAndPassword(string login, string password)
        {
            Models.User user;

            DatabaseConnection.Models.User userDBModel = userDBService.searchAndGetUserInDB(new DatabaseConnection.Models.User(login, password));
            user = new Models.User(userDBModel.Id, userDBModel.login, userDBModel.password,userDBModel.role);
            return user;
        }

        public void UpdateUserSearchedById(int id, string login, string password)
        {
            userDBService.updateUserById(id, login, password);
        }

    }
}
