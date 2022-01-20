﻿using DatabaseConnection.Models;
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

        public HashSet<BookQueue> GetBooksQueueByUserID(int userId)
        {
        openDBConnectionIfNotOpen();

            HashSet<Models.BookQueue> booksInQueue = new HashSet<Models.BookQueue>();

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
            closeDBConnection();
            return booksInQueue;
        }
    }
}
