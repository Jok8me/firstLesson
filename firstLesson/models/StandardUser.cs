using firstLesson.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.models
{
    public class StandardUser : User
    {
        public StandardUser(string login, string password) : base(login, password)
        {
        }

        public StandardUser(string login, string password, Role role) : base(login, password, role)
        {
            if (role != Role.StandardUser)
                Console.WriteLine("To create standard user u have to set role StandardUser");
            _role = Role.StandardUser;
        }
    }
}
