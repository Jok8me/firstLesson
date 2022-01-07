using DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.TableService
{
    public class BookDBService : DBConnection
    {

        public void inserBook(Book book)
        {
            openDBConnectionIfNotOpen();
            string insStmt = "INSERT INTO Books ([title], [publication_date]," +
                " [book_status_id], [book_type_id], [discount_on_book_id], [price]) " +
                " values (@title,@author,@publication_date,@status_book,@discount_type,@discount_amount,@price)";

            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = book.title;
                cmd.Parameters.Add("@publication_date", SqlDbType.Date).Value = book.publicationDate;
                cmd.Parameters.Add("@status_book", SqlDbType.TinyInt).Value = book.statusId;
                cmd.Parameters.Add("@discount_type", SqlDbType.TinyInt).Value = book.discountOnItemId;
                cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = book.price;
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();
        }

        public List<Models.BookDetails> GetBooks()
        {
            openDBConnectionIfNotOpen();

            List<Models.BookDetails> bookDetailsList = new List<Models.BookDetails>();

            //string oString = "SELECT * FROM Books WHERE id=@userId";
            string oString = "SELECT Books.id, Books.title, Authors.name, Authors.surname, Books.publication_date, BookType.book_type_name, BookStatus.book_status_name, Books.price" +
                " FROM((((Books INNER JOIN Authors_Of_Publications ON Books.id = Authors_Of_Publications.item_id)" +
                " INNER JOIN Authors ON Authors.id = Authors_Of_Publications.author_id) " +
                " INNER JOIN BookType ON BookType.id = Books.book_type_id)" +
                " INNER JOIN BookStatus ON Books.book_status_id = BookStatus.id)";

            SqlCommand command = new SqlCommand(oString, conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    bookDetailsList.Add(new Models.BookDetails(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetDateTime(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        (double)reader.GetDecimal(7)));
                }
            }
            closeDBConnection();
            return bookDetailsList;
        }

        public List<Models.BorrowedBook> GetBooksBorrowedByUserId(int userId)
        {
            openDBConnectionIfNotOpen();

            List<Models.BorrowedBook> borrowedBook = new List<Models.BorrowedBook>();

            //string oString = "SELECT * FROM Books WHERE id=@userId";
            string oString = "SELECT Books.id, Books.title, Authors.name, Authors.surname, BorrowBook.borrow_start_date, BorrowBook.borrow_end_date, BookType.book_type_name, Books.discount_on_book_id, Books.price" +
                " FROM((((((BorrowBook INNER JOIN Borrows ON BorrowBook.borrows_id = Borrows.id)" +
                " INNER JOIN Users ON Users.id = Borrows.user_id) INNER JOIN Books ON BorrowBook.book_id = Books.id)" +
                " INNER JOIN Authors_Of_Publications ON Authors_Of_Publications.item_id = Books.id)" +
                " INNER JOIN Authors ON Authors.id = Authors_Of_Publications.author_id)" +
                " INNER JOIN BookType ON BookType.id = Books.book_type_id)WHERE Borrows.user_id = @userId; ";

            SqlCommand command = new SqlCommand(oString, conn);
            command.Parameters.AddWithValue("@userId", userId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    borrowedBook.Add(new Models.BorrowedBook(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2) + " " + reader.GetString(3),
                        reader.GetDateTime(4),
                        reader.GetDateTime(5),
                        reader.GetString(6),
                        (!reader.IsDBNull(7) ? reader.GetInt32(7) : 0),
                        (double)reader.GetDecimal(8)));
                }
            }
            closeDBConnection();
            return borrowedBook;
        }

        public BookDetails GetBookByBookId(int bookId)
        {
            openDBConnectionIfNotOpen();

            BookDetails bookDetails = new BookDetails(1, "", "", "", new DateTime(), "", "", 0);

            string oString = "SELECT Books.title, Authors.name, Authors.surname, Books.publication_date, BookType.book_type_name, Books.price" +
                " FROM(((Books INNER JOIN Authors_Of_Publications ON Books.id = Authors_Of_Publications.item_id)" +
                " INNER JOIN Authors ON Authors.id = Authors_Of_Publications.author_id)" +
                " INNER JOIN BookType ON BookType.id = Books.book_type_id) WHERE books.id = @bookId";

            SqlCommand command = new SqlCommand(oString, conn);
            command.Parameters.AddWithValue("@bookId", bookId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    bookDetails.title = reader.GetString(0);
                    bookDetails.name = reader.GetString(1);
                    bookDetails.surname = reader.GetString(2);
                    bookDetails.publicationDate = reader.GetDateTime(3);
                    bookDetails.type = reader.GetString(4);
                    bookDetails.price = (double)reader.GetDecimal(5);
                }
            }
            closeDBConnection();
            return bookDetails;
        }
    }
}