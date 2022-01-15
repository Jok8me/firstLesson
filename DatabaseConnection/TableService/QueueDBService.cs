using DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.TableService
{
    public class QueueDBService : DBConnection
    {
        public List<BookQueue> GetBookQueue(int bookId)
        {
            openDBConnectionIfNotOpen();
            List<BookQueue> bookQueueList = new List<BookQueue>();
//
//            string oString = "SELECT * FROM BorrowBookQueue WHERE BorrowBookQueue.book_id = @bookId";
//
//
//           foreach (int id in booksId)
//           {
//               oStringBuilder.Append($" OR Books.id = {id}");
//           }
//           oStringBuilder.Append(";");
//           string oString = oStringBuilder.ToString();
//           SqlCommand command = new SqlCommand(oString, conn);
//           using (SqlDataReader reader = command.ExecuteReader())
//           {
//               while (reader.Read())
//               {
//                   booksInCartList.Add(new BookInCard(
//                       reader.GetInt32(0),
//                       reader.GetString(1),
//                       reader.GetString(2) + " " + reader.GetString(3),
//                       reader.GetInt32(4),
//                       reader.GetInt32(5),
//                       (double)reader.GetDecimal(6)));
//               }
//           }
//           closeDBConnection();
            return bookQueueList;
        }

    }
}
