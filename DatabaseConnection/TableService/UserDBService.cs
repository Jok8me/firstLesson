using DatabaseConnection.Models;
using System.Data;
using System.Data.SqlClient;


namespace DatabaseConnection.UsersTableServices
{
    public class UserDBService : DBConnection
    {
        public UserDBService(): base()
        {

        }

        public void insertUser(User userToInsert)
        {
            string insStmt = "INSERT INTO Users ([login], [password], [role]) " +
                "VALUES (@login,@password,@role)";
            
            openDBConnectionIfNotOpen();

            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = userToInsert.login;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = userToInsert.password;
                cmd.Parameters.Add("@role", SqlDbType.TinyInt).Value = userToInsert.role;
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();
        }

        public User searchAndGetUserInDB(User userToSearch)
        {
            openDBConnectionIfNotOpen();

            User matchingUser = new User("", "", 1);
            string oString = "SELECT * FROM Users WHERE login=@login AND password=@password";

            SqlCommand command = new SqlCommand(oString, conn);
            command.Parameters.AddWithValue("@login", userToSearch.login);
            command.Parameters.AddWithValue("@password", userToSearch.password);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    matchingUser.Id = Convert.ToInt32(reader["id"]);
                    matchingUser.login = reader["login"].ToString();
                    matchingUser.password = reader["password"].ToString();
                    matchingUser.name = reader["name"].ToString();
                    matchingUser.surname = reader["surname"].ToString();
                    matchingUser.email = reader["email"].ToString();
                    matchingUser.phoneNumber = reader["phone_number"].ToString();
                    matchingUser.role = Convert.ToInt32(reader["role"]);
                }
            }
            closeDBConnection();
            return matchingUser;
        }

        public List<Models.User> GetUsers()
        {
            openDBConnectionIfNotOpen();

            List<Models.User> usersList = new List<Models.User>();

            System.Text.StringBuilder oStringBuilder = new System.Text.StringBuilder("SELECT id, login, role FROM Users");

                SqlCommand command = new SqlCommand(oStringBuilder.ToString(), conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                    usersList.Add(new Models.User(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetByte(2)));
                }
                }

            closeDBConnection();
            return usersList;
        }

        public User searchUserByLogin(string userLogin)
        {
            User matchingUser = new User("", "", 1);
            string oString = "SELECT * FROM Users WHERE login=@login";

            openDBConnectionIfNotOpen();
            SqlCommand command = new SqlCommand(oString, conn);
            command.Parameters.AddWithValue("@login", userLogin);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    matchingUser.Id = Convert.ToInt32(reader["id"]);
                    matchingUser.login = reader["login"].ToString();
                    matchingUser.password = reader["password"].ToString();
                    matchingUser.name = reader["name"].ToString();
                    matchingUser.surname = reader["surname"].ToString();
                    matchingUser.email = reader["email"].ToString();
                    matchingUser.phoneNumber = reader["phone_number"].ToString();
                    matchingUser.role = Convert.ToInt32(reader["role"]);
                }
            }
            closeDBConnection();
            return matchingUser;
        }

        public void deleteUserById(int userId)
        {
            openDBConnectionIfNotOpen();

            string insStmt = "DELETE FROM Users WHERE id=@id";
            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = userId;
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();
        }

        public void updateUserById(int id, string login, string password, string name, string surname, string email, string phoneNumber)
        {
            openDBConnectionIfNotOpen();
            string insStmt = "UPDATE Users SET login = @login, password = @password, name = @name, surname = @surname, email = @email, phone_number = @phone_number " +
                "WHERE id = @id;";

            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = login;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@surname", SqlDbType.NVarChar).Value = surname;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                cmd.Parameters.Add("@phone_number", SqlDbType.NVarChar).Value = phoneNumber;
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();
        }
    }
}
