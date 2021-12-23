using firstLesson.enums;

namespace firstLesson.models.Users
{
    public abstract class User
    {
        internal static HashSet<User> users = new HashSet<User>();
        protected string _login;
        protected string _password;
        protected Role _role;

        public User(string login, string password)
        {
            _login = login;
            _password = password;
            _role = Role.StandardUser;
        }

        public User(string login, string password, Role role):this(login, password)
        {
            _role = role;
        }

        public void UpdateUser(string login, string password)
        {
            this._login = login;
            this._password = password;
        }

        public Role getRole()
        {
            return _role;
        }

        public string getLogin()
        {
            return _login;
        }

        public override string ToString() => "Login: " + _login + "  Role:" + _role.ToString();


        public override bool Equals(object obj)
        {
            var other = obj as User;
            if (other == null)
            {
                return false;
            }
            return _login == other._login && _password == other._password;
        }

        public override int GetHashCode() => HashCode.Combine(_login, _password);

    }
}
