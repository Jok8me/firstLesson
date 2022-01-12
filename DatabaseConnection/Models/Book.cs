using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.Models
{
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime publicationDate { get; set; }
        public int statusId { get; set; }
        public int typeId { get; set; }
        public int discountOnItemId { get; set; }
        public double price { get; set; }
        public string description { get; set; }

        public Book(int id, string title, DateTime publicationDate, int statusId, int typeId, int discountOnItemId, double price, string description)
        {
            this.id = id;
            this.title = title;
            this.publicationDate = publicationDate;
            this.statusId = statusId;
            this.typeId = typeId;
            this.discountOnItemId = discountOnItemId;
            this.price = price;
            this.description = description;
        }

        public Book(string title, DateTime publicationDate, int statusId, int typeId, double price, string description)
        {
            this.id = id;
            this.title = title;
            this.publicationDate = publicationDate;
            this.statusId = statusId;
            this.typeId = typeId;
            this.discountOnItemId = discountOnItemId;
            this.price = price;
            this.description = description;
        }

        public Book(string title, double price)
        {
            this.title = title;
            this.publicationDate = new DateTime(2000,1,1);
            this.statusId = 0;
            this.typeId = 0;
            this.price = price;
            this.description = "";
        }
    }
}
