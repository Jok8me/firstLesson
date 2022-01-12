namespace LibraryWebApp.Models
{
    //Books.id, Books.title, Books.book_status_id, " +
    //$"Books.discount_on_book_id, Books.price, BorrowBook.borrow_start_date, BorrowBook.borrow_end_date "
    public class BookInCard
    {
        public int _Id { get; set; }
        public string _Title { get; set; }
        public int _StatusId { get; set; }
        public int _DiscountId { get; set; }
        public double _Price { get; set; }
        public DateTime _BorrowStartDate { get; set; }
        public DateTime _BorrowEndDate { get; set; }

        public BookInCard(int Id, string Title, int StatusId, int DiscountId, double Price, DateTime BorrowStartDate, DateTime BorrowEndDate)
        {
            _Id = Id;
            _Title = Title;
            _StatusId = StatusId;
            _DiscountId = DiscountId;
            _Price = Price;
            _BorrowStartDate = BorrowStartDate;
            _BorrowEndDate = BorrowEndDate;
        }
    }
}
