using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.TableService
{
    public class BorrowDBService : DBConnection
    {
        public void borrowBook(int bookId, int userId)
        {
            //Borrow if book.status == 0
            //Book if book.status == 1

            openDBConnectionIfNotOpen();
            string insStmt = "EXECUTE borrowBookProcedure @userId, @bookId ,@currentDate ,@returnDate;";

            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@bookId", SqlDbType.TinyInt).Value = bookId;
                cmd.Parameters.Add("@userId", SqlDbType.TinyInt).Value = userId;
                cmd.Parameters.Add("@currentDate", SqlDbType.Date).Value = DateTime.Now.ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@returnDate", SqlDbType.Date).Value = new DateTime().AddMonths(1).ToString("yyyy-MM-dd");
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();
        }
    }
}
