using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.Models
{
    public class BookDetails
    {
        public int id;
        public string title;
        public string name;
        public string surname;
        public DateTime publicationDate;
        public string type;
        public string status;
        public double price;

        public BookDetails(int id, string title, string name, string surname, DateTime publicationDate, string type, string status, double price)
        {
            this.id = id;
            this.title = title;
            this.name = name;
            this.surname = surname;
            this.publicationDate = publicationDate;
            this.type = type;
            this.status = status;
            this.price = price;
        }

    }
}
