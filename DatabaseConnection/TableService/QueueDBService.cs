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
    public class QueueDBService : DBConnection
    {
        public void AddBookToQueue(BookInCard book, int userId)
        {
            openDBConnectionIfNotOpen();
            string insStmt = "EXECUTE addBookToQueueProcedure @bookId, @userId ,@borrowFromDate ,@borrowToDate;";

            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@bookId", SqlDbType.Int).Value = book._Id;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@borrowFromDate", SqlDbType.Date).Value = book._BorrowStartDate;
                cmd.Parameters.Add("@borrowToDate", SqlDbType.Date).Value = book._BorrowEndDate;
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();
        }

        public void RemoveBookFromQueue(int bookId, int userId)
        {
            openDBConnectionIfNotOpen();
            string insStmt = "DELETE FROM BorrowBookQueue WHERE book_id=@bookId AND user_id = @userId;";

            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@bookId", SqlDbType.Int).Value = bookId;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();
        }


        public List<BookQueue> GetBooksQueueByUserID(int userId)
        {
        openDBConnectionIfNotOpen();

            List<Models.BookQueue> booksInQueue = new List<Models.BookQueue>();

            //string oString = "SELECT * FROM Books WHERE id=@userId";
            string oString = "SELECT book_id, user_id, borrow_from_date, borrow_to_date, b.book_status_id, b.title, a.name, a.surname " +
                "FROM BorrowBookQueue " +
                "JOIN Books b ON b.id = book_id " +
                "JOIN Authors_Of_Publications aof ON aof.item_id = b.id " +
                "JOIN Authors a ON a.id = aof.author_id " +
                "WHERE user_id = @userId";

            SqlCommand command = new SqlCommand(oString, conn);
            command.Parameters.AddWithValue("@userId", userId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    BookQueue bookQueue = new BookQueue(reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetDateTime(2),
                        reader.GetDateTime(3));
                    bookQueue.setBookStatus(reader.GetInt32(4));
                    bookQueue.setTitleAndAuthor(reader.GetString(5), reader.GetString(6) + " " + reader.GetString(7));

                    booksInQueue.Add(bookQueue);
                }
            }

            // Add queue dates to BookInQueue
            StringBuilder oStringBuilder = new StringBuilder("SELECT book_id, borrow_from_date, borrow_to_date " +
                "FROM BorrowBookQueue " +
                "JOIN Books b ON b.id = book_id" +
                " JOIN Authors_Of_Publications aof ON aof.item_id = b.id " +
                "JOIN Authors a ON a.id = aof.author_id WHERE user_id != @userId AND (");

            foreach(BookQueue book in booksInQueue)
            {
                oStringBuilder.Append(" book_id = ");
                oStringBuilder.Append(book._bookId);
                oStringBuilder.Append(" OR");
            }
            if(booksInQueue.Count > 0)
            {
                oStringBuilder.Remove(oStringBuilder.Length - 2, 2);
                oStringBuilder.Append(")");
            }else oStringBuilder.Remove(oStringBuilder.Length - 5, 5);



            SqlCommand comm = new SqlCommand(oStringBuilder.ToString(), conn);
            comm.Parameters.AddWithValue("@userId", userId);
            using (SqlDataReader reader = comm.ExecuteReader())
            {
                while (reader.Read())
                {
                    int bookId = reader.GetInt32(0);
                    BookDateRange bookDateRange = new BookDateRange(
                        reader.GetDateTime(1),
                        reader.GetDateTime(2));
                    booksInQueue.Where(x => x._bookId == bookId).Select(x => { x.addDatesToDateRanges(bookDateRange); return x; }).ToList();
                }
            }


            // Add borrow_date to BookInQueue
            StringBuilder oStringBuilderAddBorrowDate = new StringBuilder("SELECT book_id, borrow_start_date, borrow_end_date " +
                "FROM BorrowBook " +
                "WHERE returned = 0  AND (");

            foreach (BookQueue book in booksInQueue)
            {
                oStringBuilderAddBorrowDate.Append(" book_id = ");
                oStringBuilderAddBorrowDate.Append(book._bookId);
                oStringBuilderAddBorrowDate.Append(" OR");
            }
            if (booksInQueue.Count > 0)
            {
                oStringBuilderAddBorrowDate.Remove(oStringBuilderAddBorrowDate.Length - 2, 2);
                oStringBuilderAddBorrowDate.Append(")");
            }
            else
            {
                oStringBuilderAddBorrowDate.Remove(oStringBuilderAddBorrowDate.Length - 6, 6);
            }



            SqlCommand commAddBorrowDate = new SqlCommand(oStringBuilderAddBorrowDate.ToString(), conn);
            using (SqlDataReader reader = commAddBorrowDate.ExecuteReader())
            {
                while (reader.Read())
                {
                    int bookId = reader.GetInt32(0);
                    BookDateRange bookDateRange = new BookDateRange(
                        reader.GetDateTime(1),
                        reader.GetDateTime(2));
                    booksInQueue.Where(x => x._bookId == bookId).Select(x => { x.addDatesToDateRanges(bookDateRange); return x; }).ToList();
                }
            }



            closeDBConnection();
            return booksInQueue;
        }

        public void UpdateDateBookInQueue(int bookId, int userId, DateTime newStartDate, DateTime newEndDate)
        {
            openDBConnectionIfNotOpen();
            string insStmt = "UPDATE BorrowBookQueue SET borrow_from_date = @newStartDate, borrow_to_date = @newEndDate WHERE book_id = @bookId AND user_id = @userId;";

            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@bookId", SqlDbType.Int).Value = bookId;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@newStartDate", SqlDbType.DateTime).Value = newStartDate;
                cmd.Parameters.Add("@newEndDate", SqlDbType.DateTime).Value = newEndDate;
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();
        }
    }
}
