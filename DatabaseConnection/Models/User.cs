using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.Models
{
    public class User
    {
        public int Id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int role { get; set; }

        public User(string login, string password, int role)
        {
            this.login = login;
            this.password = password;
            this.role = role;
        }

        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
            role = 1;
        }
    }
}
