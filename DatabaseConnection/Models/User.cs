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
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
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

        public User(string login, string password, string name, string surname, string email, string phoneNumber)
        {
            this.login=login;
            this.password=password;
            this.name = name;
            this.surname = surname;
            this.email = email;
            role = 1;
        }

        public User(int id, string login, int role)
        {
            this.Id = id;
            this.login = login;
            this.role=role;
            this.password = "none";
            this.name = "none";
            this.surname = "none";
            this.email = "none";
            this.phoneNumber = "none";
        }
    }
}
