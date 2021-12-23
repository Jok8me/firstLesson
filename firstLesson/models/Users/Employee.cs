using firstLesson.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.models.Users
{
    public class Employee : User
    {
        public Employee(string login, string password, Role role) : base(login, password)
        {
            this._role = Role.Employee;
        }
    }
}
