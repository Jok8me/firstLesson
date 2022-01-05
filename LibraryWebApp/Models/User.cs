namespace LibraryWebApp.Models
{
    public class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int role { get; set; }

        public User(int id, string login, int role)
        {
            this.id = id;
            this.login = login;
            this.role = role;
        }

        public User(int id, string login, string password, int role)
        {
            this.login = login;
            this.password = password;
            this.id=id;
            this.role=role;
        }
    }
}
