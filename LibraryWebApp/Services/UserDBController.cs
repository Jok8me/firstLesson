using DatabaseConnection.UsersTableServices;
using DatabaseConnection.Models;

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
            user = new Models.User(userDBModel.Id, userDBModel.login, userDBModel.password, userDBModel.name, userDBModel.surname, userDBModel.email, userDBModel.phoneNumber, userDBModel.role);
            return user;
        }

        public void UpdateUserSearchedById(int id, string login, string password, string name, string surname, string email, string phoneNumber)
        {
            userDBService.updateUserById(id, login, password, name, surname, email, phoneNumber);
        }

        public List<DatabaseConnection.Models.User> GetUsers()
        {
            return userDBService.GetUsers();
        }

    }
}
