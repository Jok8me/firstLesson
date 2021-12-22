using firstLesson.enums;

namespace firstLesson.models
{
    public abstract class User
    {
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

        public Role getRole()
        {
            return _role;
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
