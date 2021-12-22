using firstLesson.enums;

namespace firstLesson.models
{
    public class Admin : User
    {
        public Admin(string login, string password) : base(login, password)
        {
            _role = Role.Admin;
        }

        public Admin(string login, string password, Role role) : base(login, password)
        {
            _role = Role.Admin;
        }
    }
}
