namespace LibraryWebApp.Models
{
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public DateTime publicationDate { get; set; }
        public int statusId { get; set; }
        public int typeId { get; set; }
        public int discountOnItemId { get; set; }

        public Book(int id, string title, DateTime publicationDate, int statusId, int typeId, int discountOnItemId, double price)
        {

        this.id = id;
        this.title = title;
        this.price = price;
        this.publicationDate = publicationDate;
        this.statusId = statusId;
        this.typeId = typeId;
        this.discountOnItemId = discountOnItemId;

        }

    }
}
