﻿using DatabaseConnection.Models.DiscountStrategies;
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
        public int rate;
        public int typeId;
        public int categoryId;

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
            this.rate = 0;
            this.typeId = 0;
            this.categoryId = 0;


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

        public void SetTypeAndCategory(int v1, int v2)
        {
            this.typeId = v1;
            this.categoryId = v2;
        }
    }
}
