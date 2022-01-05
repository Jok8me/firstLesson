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

        public List<Models.Book> GetBooksBorrowedByUserId(int userId)
        {
            openDBConnectionIfNotOpen();

            List<Models.Book> books = new List<Models.Book>();

            string oString = "SELECT * FROM Books WHERE id=@userId";

            SqlCommand command = new SqlCommand(oString, conn);
            command.Parameters.AddWithValue("@userId", userId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    books.Add(new Models.Book(reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetDateTime(2),
                        reader.GetInt32(3),
                        reader.GetInt32(4),
                        (!reader.IsDBNull(5) ? reader.GetInt32(5) : 0),
                        (double)reader.GetDecimal(6)));
                }
            }
            closeDBConnection();
            return books;
        }
    }
}