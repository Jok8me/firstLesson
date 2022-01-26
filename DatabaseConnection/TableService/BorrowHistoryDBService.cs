using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.TableService
{
    public class BorrowHistoryDBService : DBConnection
    {
        public List<Models.BookInHistory> GetBooksHistoryByUserId(int userId)
        {
            openDBConnectionIfNotOpen();

            List<Models.BookInHistory> bookHistoryList = new List<Models.BookInHistory>();

            string oString = "SELECT b.id, b.title, a.name, a.surname, b.publication_date, bType.book_type_name, c.category_name, b.description, bb.borrow_start_date, bb.borrow_end_date, b.price, bb.return_date" +
                " FROM Books b" +
                " JOIN BorrowBook bb ON bb.book_id = b.id" +
                " JOIN Borrows ON borrows.id = bb.borrows_id" +
                " JOIN Authors_Of_Publications aof ON aof.item_id = b.id" +
                " JOIN Authors a ON a.id = aof.author_id" +
                " JOIN BookType bType ON bType.id = b.book_type_id" +
                " JOIN CategoryOfBook cob ON cob.book_id = b.id" +
                " JOIN Category c ON c.id = cob.category_id" +
                " WHERE bb.returned = 1 AND Borrows.user_id = @userID";

            SqlCommand command = new SqlCommand(oString, conn);

            command.Parameters.Add("@userID", SqlDbType.Int).Value = userId;

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    bookHistoryList.Add(new Models.BookInHistory(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetDateTime(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetDateTime(8),
                        reader.GetDateTime(9),
                        (double)reader.GetDecimal(10),
                        reader.GetDateTime(11)));
                }
            }
            closeDBConnection();
            return bookHistoryList;
        }
    }
}
