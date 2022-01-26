using DatabaseConnection.Models.DiscountStrategies;
using firstLesson.services.DiscountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.Models
{
    public class BookOfTheDay
    {
        public int _id { get; set; }
        public string _name { get; set; }
        public string _surname { get; set; }
        public string _title { get; set; }
        public int _discountType { get; set; }
        public double _discountAmount { get; set; }
        public double _price { get; set; }
        public string _description { get; set; }
        public double _botdDiscountAmmount { get; set; }
        public double _priceAfterDiscount { get; set; }

        public BookOfTheDay(int id, string name, string surname, string title, int discountType, double discountAmount, double price, string description, double botdDiscountAmmount)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _title = title;
            _discountType = discountType;
            _discountAmount = discountAmount;
            _price = price;
            _description = description;
            _botdDiscountAmmount = botdDiscountAmmount;

            List<IDiscountStrategy> botdDiscounts = new List<IDiscountStrategy>();
            PercentageDiscountStrategy botdDiscountStrategy = new PercentageDiscountStrategy(_botdDiscountAmmount);

            switch (this._discountType)
                {
                    case 0:
                        botdDiscounts.Add(new PriceDiscountStrategy(this._discountAmount));
                        break;
                    case 1:
                        botdDiscounts.Add(new PercentageDiscountStrategy(this._discountAmount));
                        break;
                    default:
                        botdDiscounts.Add(new PriceDiscountStrategy(this._discountAmount));
                        break;
                }
            botdDiscounts.Add(botdDiscountStrategy);

            ApplyAllDiscount applyAllDiscount = new ApplyAllDiscount(botdDiscounts);

            _priceAfterDiscount = applyAllDiscount.calculate(_price);


        }

        public BookOfTheDay()
        {

        }

    }
}
