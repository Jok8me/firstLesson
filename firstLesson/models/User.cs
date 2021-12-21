using firstLesson.enums;

namespace firstLesson.models
{
    public class User
    {
        private string _login;
        private string _password;
        private Role _role;

        public User(string login, string password)
        {
            _login = login;
            _password = password;
            _role = Role.User;
        }

        public User(string login, string password, Role role):this(login, password)
        {
            _role = role;
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
