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
    public class BookDBService : DBConnection
    {

        public void inserBook(Book book)
        {
            openDBConnectionIfNotOpen();
            string insStmt = "INSERT INTO Books ([title], [publication_date]," +
                " [book_status_id], [book_type_id], [discount_on_book_id], [price], [description]) " +
                " values (@title,@author,@publication_date,@status_book,@discount_type,@discount_amount,@price,@description)";

            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = book.title;
                cmd.Parameters.Add("@publication_date", SqlDbType.Date).Value = book.publicationDate;
                cmd.Parameters.Add("@status_book", SqlDbType.TinyInt).Value = book.statusId;
                cmd.Parameters.Add("@discount_type", SqlDbType.TinyInt).Value = book.discountOnItemId;
                cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = book.price;
                cmd.Parameters.Add("@description", SqlDbType.Decimal).Value = book.description;
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();
        }

        public void ReturnBookById(int bookId)
        {
            //New
                openDBConnectionIfNotOpen();

                string insStmt = "DELETE FROM Users WHERE id=@id";
                using (SqlCommand cmd = new SqlCommand(insStmt, conn))
                {
                    //cmd.Parameters.Add("@id", SqlDbType.Int).Value = userId;
                    cmd.ExecuteNonQuery();
                }
                closeDBConnection();

        }

        public List<Models.BookDetails> GetBooksByTypeAndCategoryAndSearchInput(int bookType, List<int> bookCategory, string searchInput)
        {
            openDBConnectionIfNotOpen();

            List<Models.BookDetails> bookDetailsList = new List<Models.BookDetails>();

            //string oString = "SELECT * FROM Books WHERE id=@userId";
            StringBuilder stringBuilder = new StringBuilder("SELECT Books.id, Books.title, Authors.name, Authors.surname, Books.publication_date, BookType.book_type_name,Category.category_name ,BookStatus.book_status_name, Books.price, Books.description " +
                "FROM((((((Books INNER JOIN Authors_Of_Publications ON Books.id = Authors_Of_Publications.item_id) " +
                "INNER JOIN Authors ON Authors.id = Authors_Of_Publications.author_id) " +
                "INNER JOIN BookType ON BookType.id = Books.book_type_id) " +
                "INNER JOIN BookStatus ON Books.book_status_id = BookStatus.id) " +
                "INNER JOIN CategoryOfBook ON CategoryOfBook.book_id = Books.id) " +
                "INNER JOIN Category ON Category.id = CategoryOfBook.category_id) ");

            if (!(bookType == 0 && bookCategory.ElementAt(0) == 0))
            {
                stringBuilder.Append(" WHERE ");

                //Books.title LIKE '%%' OR Authors.name LIKE '%%' OR Authors.surname LIKE '%%'
                stringBuilder.Append("(Books.title LIKE '%");
                stringBuilder.Append(searchInput);
                stringBuilder.Append("%' OR Authors.name LIKE '%");
                stringBuilder.Append(searchInput);
                stringBuilder.Append("%' OR Authors.surname LIKE '%");
                stringBuilder.Append(searchInput);
                stringBuilder.Append("%') AND ");

                if (bookType != 0)
                {
                    stringBuilder.Append("Books.book_type_id = ");
                    stringBuilder.Append((bookType - 1).ToString());
                    stringBuilder.Append(" ");
                }

                if (bookType > 0 && bookCategory.Count > 0 && bookCategory.ElementAt(0) > 0)
                    stringBuilder.Append("AND ");

                if (!(bookCategory.Count == 1 && bookCategory.ElementAt(0) == 0))
                {
                    stringBuilder.Append("(");
                    foreach (int category in bookCategory)
                    {
                        stringBuilder.Append(" CategoryOfBook.category_id = ");
                        stringBuilder.Append(category.ToString());
                        stringBuilder.Append(" OR");
                    }
                    stringBuilder.Remove(stringBuilder.Length - 2, 2);
                    if (bookCategory.Count > 0)
                        stringBuilder.Append(")");
                }

            } else if (!String.IsNullOrEmpty(searchInput))
            {
                stringBuilder.Append(" WHERE ");
                stringBuilder.Append("(Books.title LIKE '%");
                stringBuilder.Append(searchInput);
                stringBuilder.Append("%' OR Authors.name LIKE '%");
                stringBuilder.Append(searchInput);
                stringBuilder.Append("%' OR Authors.surname LIKE '%");
                stringBuilder.Append(searchInput);
                stringBuilder.Append("%')");
            }

            SqlCommand command = new SqlCommand(stringBuilder.ToString(), conn);

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
                        reader.GetString(7),
                        (double)reader.GetDecimal(8),
                        reader.GetString(9)));
                }
            }
            closeDBConnection();
            return bookDetailsList;
        }



        public void BookBookByUser(int bookId, int userId, DateTime fromDate, DateTime toDate)
        {
            openDBConnectionIfNotOpen();

            string insStmt = "INSERT INTO BorrowBookQueue ([book_id], [user_id], [borrow_from_date], [borrow_to_date]) " +
                " values (@bookId,@userId,@fromDate,@toDate)";

            using (SqlCommand cmd = new SqlCommand(insStmt, conn))
            {
                cmd.Parameters.Add("@bookId", SqlDbType.Int).Value = bookId;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@fromDate", SqlDbType.Date).Value = fromDate;
                cmd.Parameters.Add("@toDate", SqlDbType.Date).Value = toDate;
                cmd.ExecuteNonQuery();
            }
            closeDBConnection();

        }

        public List<BookInCard> getBookByBookIdInIntList(List<int> booksId)
        {
            openDBConnectionIfNotOpen();

            List<BookInCard> booksInCartList = new List<BookInCard>();

            System.Text.StringBuilder oStringBuilder = new System.Text.StringBuilder($"SELECT Books.id, Books.title, Authors.name, Authors.surname, Books.book_status_id, Books.discount_on_book_id, Books.price" +
                $" FROM(Books INNER JOIN Authors_Of_Publications ON Authors_Of_Publications.item_id = Books.id)" +
                $" INNER JOIN Authors ON Authors_Of_Publications.author_id = Authors.id WHERE Books.id = { booksId.ElementAt(0).ToString()}");
            foreach(int id in booksId)
            {
                oStringBuilder.Append($" OR Books.id = {id}");
            }
            oStringBuilder.Append(";");
            string oString = oStringBuilder.ToString();
            SqlCommand command = new SqlCommand(oString, conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    booksInCartList.Add(new BookInCard(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2) + " " + reader.GetString(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5),
                        (double)reader.GetDecimal(6)));
                }
            }
            closeDBConnection();
            return booksInCartList;
        }

        public List<Models.BookDetails> GetBooks()
        {
            openDBConnectionIfNotOpen();

            List<Models.BookDetails> bookDetailsList = new List<Models.BookDetails>();

            //string oString = "SELECT * FROM Books WHERE id=@userId";
            string oString = "SELECT Books.id, Books.title, Authors.name, Authors.surname, Books.publication_date, BookType.book_type_name,Category.category_name ,BookStatus.book_status_name, Books.price, Books.description " +
                "FROM((((((Books INNER JOIN Authors_Of_Publications ON Books.id = Authors_Of_Publications.item_id) " +
                "INNER JOIN Authors ON Authors.id = Authors_Of_Publications.author_id) " +
                "INNER JOIN BookType ON BookType.id = Books.book_type_id) " +
                "INNER JOIN BookStatus ON Books.book_status_id = BookStatus.id) " +
                "INNER JOIN CategoryOfBook ON CategoryOfBook.book_id = Books.id) " +
                "INNER JOIN Category ON Category.id = CategoryOfBook.category_id)";

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
                        reader.GetString(7),
                        (double)reader.GetDecimal(8),
                        reader.GetString(9)));
                }
            }
            closeDBConnection();
            return bookDetailsList;
        }

        public List<Models.BorrowedBook> GetBooksCurrentBorrowedByUserId(int userId)
        {
            openDBConnectionIfNotOpen();

            List<Models.BorrowedBook> borrowedBook = new List<Models.BorrowedBook>();

            //string oString = "SELECT * FROM Books WHERE id=@userId";
            string oString = "SELECT Books.id, Books.title, Authors.name, Authors.surname, BorrowBook.borrow_start_date, BorrowBook.borrow_end_date, BookType.book_type_name, Books.discount_on_book_id, Books.price" +
                " FROM((((((BorrowBook INNER JOIN Borrows ON BorrowBook.borrows_id = Borrows.id)" +
                " INNER JOIN Users ON Users.id = Borrows.user_id) INNER JOIN Books ON BorrowBook.book_id = Books.id)" +
                " INNER JOIN Authors_Of_Publications ON Authors_Of_Publications.item_id = Books.id)" +
                " INNER JOIN Authors ON Authors.id = Authors_Of_Publications.author_id)" +
                " INNER JOIN BookType ON BookType.id = Books.book_type_id)WHERE Borrows.user_id = @userId AND BorrowBook.returned = 'FALSE'; ";

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

            BookDetails bookDetails = new BookDetails(1, "", "", "",new DateTime(), "", "","" , 0, "");

            string oString = "SELECT Books.title, Authors.name, Authors.surname, Books.publication_date, BookType.book_type_name, Category.category_name ,Books.price, Books.description " +
                "FROM(((((Books INNER JOIN Authors_Of_Publications ON Books.id = Authors_Of_Publications.item_id) " +
                "INNER JOIN Authors ON Authors.id = Authors_Of_Publications.author_id) " +
                "INNER JOIN BookType ON BookType.id = Books.book_type_id) " +
                "INNER JOIN CategoryOfBook ON CategoryOfBook.book_id = Books.id) " +
                "INNER JOIN Category ON Category.id = CategoryOfBook.category_id) WHERE books.id = @bookId";

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
                    bookDetails.category = reader.GetString(5);
                    bookDetails.price = (double)reader.GetDecimal(6);
                    bookDetails.description = reader.GetString(7);
                }
            }
            closeDBConnection();
            return bookDetails;
        }

    }
}