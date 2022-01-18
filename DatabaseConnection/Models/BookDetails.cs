using DatabaseConnection.Models.DiscountStrategies;
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
        public string category;
        public string status;
        public int discountType;
        public double discountAmmount;
        public double priceAfterDiscount;
        public double price;
        public string description;

        public BookDetails(int id, string title, string name, string surname, DateTime publicationDate, string type, string category, string status, int discountType, double discountAmmount, double price, string description)
        {
            this.id = id;
            this.title = title;
            this.name = name;
            this.surname = surname;
            this.publicationDate = publicationDate;
            this.type = type;
            this.category = category;
            this.status = status;
            this.discountType = discountType;
            this.discountAmmount = discountAmmount;
            this.price = price;
            this.description = description;


            if (this.discountAmmount != 0)
            {
                IDiscountStrategy discountStrategy;
                switch (this.discountType)
                {
                    case 0:
                        discountStrategy = new PriceDiscountStrategy(this.discountAmmount);
                        break;
                    case 1:
                        discountStrategy = new PercentageDiscountStrategy(this.discountAmmount);
                        break;
                    default:
                        discountStrategy = new PriceDiscountStrategy(this.discountAmmount);
                        break;
                }
                this.priceAfterDiscount = discountStrategy.calculate(this.price);

            }
            else this.priceAfterDiscount = 0;
        }
    }
}
