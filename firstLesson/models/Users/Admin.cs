using firstLesson.enums;
using firstLesson.models.Users;

namespace firstLesson.models.Users
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
