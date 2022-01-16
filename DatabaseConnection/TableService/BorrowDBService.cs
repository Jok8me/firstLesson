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
    public class BorrowDBService : DBConnection
    {
        public void borrowBook(BookInCard book, int userId)
        {
            //Borrow if book.status == 0
            //Book if book.status == 1

            openDBConnectionIfNotOpen();
            string insStmt = "EXECUTE borrowBookProcedure @userId, @bookId ,@currentDate ,@returnDate;";

            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@bookId", SqlDbType.TinyInt).Value = book._Id;
                cmd.Parameters.Add("@userId", SqlDbType.TinyInt).Value = userId;
                cmd.Parameters.Add("@currentDate", SqlDbType.Date).Value = book._BorrowStartDate;
                cmd.Parameters.Add("@returnDate", SqlDbType.Date).Value = book._BorrowEndDate;
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();
        }

        public void ReturnBookByBookIdAndUserId(int bookId, int userId)
        {
            //Borrow if book.status == 0
            //Book if book.status == 1

            openDBConnectionIfNotOpen();
            string insStmt = "EXECUTE returnBookProcedure @userId, @bookId ,@cash;";

            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@bookId", SqlDbType.TinyInt).Value = bookId;
                cmd.Parameters.Add("@userId", SqlDbType.TinyInt).Value = userId;
                cmd.Parameters.Add("@cash", SqlDbType.Decimal).Value = 1.0;
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();
        }

        public HashSet<BorrowedBook> GetBorrowedBookByBooksID(List<int> booksID)
        {
            openDBConnectionIfNotOpen();
            HashSet<BorrowedBook> borrowedBooks = new HashSet<BorrowedBook>();


            StringBuilder oString = new StringBuilder("SELECT Books.id, Books.title, Authors.name, Authors.surname, BorrowBook.borrow_start_date, BorrowBook.borrow_end_date, BookType.book_type_name, Books.discount_on_book_id, Books.price" +
                " FROM((((((BorrowBook INNER JOIN Borrows ON BorrowBook.borrows_id = Borrows.id)" +
                " INNER JOIN Users ON Users.id = Borrows.user_id) INNER JOIN Books ON BorrowBook.book_id = Books.id)" +
                " INNER JOIN Authors_Of_Publications ON Authors_Of_Publications.item_id = Books.id)" +
                " INNER JOIN Authors ON Authors.id = Authors_Of_Publications.author_id)" +
                " INNER JOIN BookType ON BookType.id = Books.book_type_id) WHERE BorrowBook.returned = 'FALSE' ");

            if(booksID != null)
            {
                oString.Append(" AND (");
                foreach (int bookID in booksID)
                {
                    oString.Append(" Books.id = ");
                    oString.Append(bookID);
                    oString.Append(" OR");
                }
                oString.Remove(oString.Length - 2, 2);
                oString.Append(");");
            }

            SqlCommand command = new SqlCommand(oString.ToString(), conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    borrowedBooks.Add(new Models.BorrowedBook(
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
            return borrowedBooks;
        }

        public Dictionary<int, int> CountBookQueueByBookIDs()
        {
            Dictionary<int,int> countBooksInQueue = new Dictionary<int, int>();
            StringBuilder oString = new StringBuilder("SELECT BorrowBookQueue.book_id, COUNT(BorrowBookQueue.book_id) AS count FROM BorrowBookQueue GROUP BY BorrowBookQueue.book_id");
            
            openDBConnectionIfNotOpen();

            SqlCommand command = new SqlCommand(oString.ToString(), conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    countBooksInQueue.Add(reader.GetInt32(0), reader.GetInt32(1));
                }
            }

            closeDBConnection();
            return countBooksInQueue;
        }
    

        public HashSet<BookQueue> GetBookQueueByBooksID(List<int> booksId)
        {
            HashSet<BookQueue> borrowedBooks = new HashSet<BookQueue>();
            StringBuilder oString = new StringBuilder("SELECT BorrowBookQueue.book_id, BorrowBookQueue.user_id, BorrowBookQueue.borrow_from_date, " +
                "BorrowBookQueue.borrow_to_date FROM BorrowBookQueue ");

            if (booksId != null)
            {
                openDBConnectionIfNotOpen();

                oString.Append(" WHERE (");
                foreach (int bookID in booksId)
                {
                    oString.Append(" BorrowBookQueue.book_id = ");
                    oString.Append(bookID);
                    oString.Append(" OR");
                }
                oString.Remove(oString.Length - 2, 2);
                oString.Append(");");

                SqlCommand command = new SqlCommand(oString.ToString(), conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        borrowedBooks.Add(new Models.BookQueue(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetDateTime(2),
                            reader.GetDateTime(3)));
                    }
                }

                closeDBConnection();
            }
            return borrowedBooks;
        }
    }
}
