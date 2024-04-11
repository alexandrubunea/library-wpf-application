using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library_Application.Models;
using System.Security.Policy;
using Library_Application.Views;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library_Application.Database
{
    internal class DBUtils
    {
        // public
        public static SqlConnection Connection
        {
            get => new SqlConnection(connectionString);
        }

        public static void removeAuthorsForBook(int BookId)
        {
            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("removeAuthorsForBook", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookId", BookId);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public static void decreaseBookStock(int BookId)
        {
            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("decreaseBookStock", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookId", BookId);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public static void increaseBookStock(int BookId)
        {
            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("increaseBookStock", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookId", BookId);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public static int countBookTypeBooks(int bookTypeId)
        {
            int result = -1;

            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("countBookTypeBooks", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookTypeId", bookTypeId);

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return result;
        }

        public static int countAuthorBooks(int authorId)
        {
            int result = -1;

            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("countAuthorBooks", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AuthorId", authorId);

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return result;
        }

        public static int countPublisherBooks(int publisherId)
        {
            int result = -1;

            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("countPublisherBooks", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PublisherId", publisherId);

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return result;
        }

        public static int getLastBookId()
        {
            int result = -1;

            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("getLastBookId", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return result;
        }

        public static Models.User? retriveUserData(int Id)
        {
            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("retriveUserData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);

            Models.User? user = null;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    string email = reader.GetString(3);
                    string phone = reader.GetString(4);
                    bool active = reader.GetBoolean(5);
                    int acccesLevel = reader.GetInt32(6);

                    user = new Models.User(
                            firstName,
                            lastName,
                            email,
                            phone,
                            acccesLevel
                        );
                }
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return user;
        }

        public static Models.Publisher? getPublisher(int id)
        {
            Models.Publisher? publisher = null;

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("getPublisher", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) {
                    string? publisherName = Convert.ToString(reader["Name"]);
                    bool? publisherActive = Convert.ToBoolean(reader["Active"]);

                    if(publisherName != null && publisherActive != null )
                    {
                        publisher = new Models.Publisher(publisherName);
                        publisher.Active = (bool) publisherActive;
                        publisher.Id = id;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return publisher;
        }

        public static BookType? getBookType(int id)
        {
            BookType? bookType = null;

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("getBookType", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string? bookTypeName = Convert.ToString(reader["Name"]);
                    bool? bookTypeActive = Convert.ToBoolean(reader["Active"]);

                    if (bookTypeName != null && bookTypeActive != null)
                    {
                        bookType = new BookType(bookTypeName);
                        bookType.Active = (bool)bookTypeActive;
                        bookType.Id = id;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return bookType;
        }

        public static Author? getAuthor(int id)
        {
            Author? author = null;

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("getAuthor", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AuthorId", id);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string? AuthorFirstName = Convert.ToString(reader["FirstName"]);
                    string? AuthorLastName = Convert.ToString(reader["LastName"]);
                    string? AuthorBirth = Convert.ToString(reader["BirthDate"]);

                    if (AuthorFirstName != null && AuthorLastName != null && AuthorBirth != null)
                    {
                        DateOnly dateOnly = DateOnly.FromDateTime(Convert.ToDateTime(AuthorBirth));

                        author = new Author(AuthorFirstName, AuthorLastName, dateOnly.ToString("dd/MM/yyyy"));

                        author.Id = Convert.ToInt32(reader["AuthorId"]);
                        author.Active = Convert.ToBoolean(reader["Active"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return author;
        }

        public static RestrictedDataUser getUserById(int UserId)
        {
            RestrictedDataUser user = new RestrictedDataUser(UserId, string.Empty, string.Empty, string.Empty, string.Empty, false);

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("getUserById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string? userFirstName = Convert.ToString(reader["FirstName"]);
                    string? userLastName = Convert.ToString(reader["LastName"]);
                    string? userEmail = Convert.ToString(reader["Email"]);
                    string? userPhone = Convert.ToString(reader["Phone"]);
                    bool userActive = Convert.ToBoolean(reader["Active"]);

                    if (userFirstName != null && userLastName != null && userEmail != null && userPhone != null)
                        user = new RestrictedDataUser(UserId, userFirstName, userLastName, userEmail, userPhone, userActive);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return user;
        }

        public static Book getBookById(int BookId)
        {
            Book book = new Book(string.Empty, string.Empty, new BookType(string.Empty), new Models.Publisher(string.Empty), new List<Author>(), 0);

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("getBookById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookId", BookId);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    BookType? bookType = getBookType(Convert.ToInt32(reader["BookTypeId"]));
                    Models.Publisher? publisher = getPublisher(Convert.ToInt32(reader["PublisherId"]));
                    string? bookTitle = Convert.ToString(reader["Title"]);
                    string? bookPublishYear = Convert.ToString(reader["PublishYear"]);
                    int? bookStock = Convert.ToInt32(reader["Stock"]);
                    bool bookActive = Convert.ToBoolean(reader["Active"]);
                    List<Author> authorList = new List<Author>(getBookAuthors(BookId));

                    if (bookTitle != null && bookPublishYear != null && bookType != null && publisher != null && bookStock != null)
                    {
                        DateOnly dateOnly = DateOnly.FromDateTime(Convert.ToDateTime(bookPublishYear));

                        book = new Book(bookTitle, dateOnly.ToString("dd/MM/yyyy"), bookType, publisher, authorList, (int)bookStock);
                        book.Active = bookActive;
                        book.Id = BookId;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return book;
        }

        public static List<UserBook> getUserBorrows(int UserId)
        {
            List<UserBook> borrows = new List<UserBook>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("getUserBooks", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int? id = Convert.ToInt32(reader["Id"]);
                    RestrictedDataUser? user = getUserById(Convert.ToInt32(reader["UserId"]));
                    Book? book = getBookById(Convert.ToInt32(reader["BookId"]));
                    string? startDate = Convert.ToDateTime(reader["StartDate"]).ToString("dd/MM/yyyy");
                    string? returnDate = Convert.ToDateTime(reader["ReturnDate"]).ToString("dd/MM/yyyy");
                    bool active = Convert.ToBoolean(reader["Active"]);

                    if(id != null && user != null && book != null && startDate != null && returnDate != null)
                    {
                        UserBook borrow = new UserBook(user, book, startDate, returnDate);
                        borrow.Id = (int) id;
                        borrows.Add(borrow);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return borrows;
        }

        public static List<UserBook> retriveBookBorrows()
        {
            List<UserBook> borrows = new List<UserBook>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("retriveUserBook", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int? id = Convert.ToInt32(reader["Id"]);
                    RestrictedDataUser? user = getUserById(Convert.ToInt32(reader["UserId"]));
                    Book? book = getBookById(Convert.ToInt32(reader["BookId"]));
                    string? startDate = Convert.ToDateTime(reader["StartDate"]).ToString("dd/MM/yyyy");
                    string? returnDate = Convert.ToDateTime(reader["ReturnDate"]).ToString("dd/MM/yyyy");
                    bool active = Convert.ToBoolean(reader["Active"]);

                    if (id != null && user != null && book != null && startDate != null && returnDate != null)
                    {
                        UserBook borrow = new UserBook(user, book, startDate, returnDate);
                        borrow.Id = (int)id;
                        borrows.Add(borrow);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return borrows;
        }

        public static List<RestrictedDataUser> retriveUsers()
        {
            List<RestrictedDataUser > users = new List<RestrictedDataUser>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("retriveUsers", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int? userId = Convert.ToInt32(reader["UserId"]);
                    string? userFirstName = Convert.ToString(reader["FirstName"]);
                    string? userLastName = Convert.ToString(reader["LastName"]);
                    string? userEmail = Convert.ToString(reader["Email"]);
                    string? userPhone = Convert.ToString(reader["Phone"]);
                    bool userActive = Convert.ToBoolean(reader["Active"]);

                    if(userId != null && userFirstName != null && userLastName != null && userEmail != null && userPhone != null)
                        users.Add(new RestrictedDataUser((int) userId, userFirstName, userLastName, userEmail, userPhone, userActive));
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return users;
        }

        public static List<Author> getBookAuthors(int id)
        {
            List<Author> bookAuthors = new List<Author>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("retriveBookAuthors", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookId", id);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int? authorId = Convert.ToInt32(reader["AuthorId"]);

                    if(authorId !=  null)
                    {
                        Author? author = getAuthor((int)authorId);

                        if(author != null)
                            bookAuthors.Add(author);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return bookAuthors;
        }

        public static List<BookType> retriveActiveBookTypes()
        {
            List<BookType> bookTypes = new List<BookType>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("retriveActiveBookTypes", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int? bookTypeId = Convert.ToInt32(reader["BookTypeId"]);
                    string? bookTypeTitle = Convert.ToString(reader["Name"]);
                    bool bookTypeActive = Convert.ToBoolean(reader["Active"]);

                    if (bookTypeId != null && bookTypeTitle != null)
                    {
                        BookType bookType = new BookType(bookTypeTitle);
                        bookType.Active = bookTypeActive;
                        bookType.Id = (int)bookTypeId;
                        bookTypes.Add(bookType);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return bookTypes;
        }

        public static List<Models.Publisher> retriveActivePublishers()
        {
            List<Models.Publisher> publishers = new List<Models.Publisher>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("retriveActivePublishers", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int? publisherId = Convert.ToInt32(reader["PublisherId"]);
                    string? publisherTitle = Convert.ToString(reader["Name"]);
                    bool publisherActive = Convert.ToBoolean(reader["Active"]);

                    if (publisherId != null && publisherTitle != null)
                    {
                        Models.Publisher publisher = new Models.Publisher(publisherTitle);
                        publisher.Active = publisherActive;
                        publisher.Id = (int)publisherId;
                        publishers.Add(publisher);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return publishers;
        }

        public static List<Author> retriveActiveAuthors()
        {
            List<Author> authors = new List<Author>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("retriveActiveAuthors", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int? authorId = Convert.ToInt32(reader["AuthorId"]);
                    string? authorFirstName = Convert.ToString(reader["FirstName"]);
                    string? authorLastName = Convert.ToString(reader["LastName"]);
                    string? authorBirthDate = Convert.ToString(reader["BirthDate"]);
                    bool authorActive = Convert.ToBoolean(reader["Active"]);

                    if (authorId != null && authorFirstName != null && authorLastName != null && authorBirthDate != null)
                    {
                        DateOnly dateOnly = DateOnly.FromDateTime(Convert.ToDateTime(authorBirthDate));

                        Author author = new Author(authorFirstName, authorLastName, dateOnly.ToString("dd/MM/yyyy"));
                        author.Active = authorActive;
                        author.Id = (int) authorId;
                        authors.Add(author);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return authors;
        }

        public static List<Book> retriveActiveBooks()
        {
            List<Book> books = new List<Book>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("retriveActiveBooks", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int? bookId = Convert.ToInt32(reader["BookId"]);
                    BookType? bookType = getBookType(Convert.ToInt32(reader["BookTypeId"]));
                    Models.Publisher? publisher = getPublisher(Convert.ToInt32(reader["PublisherId"]));
                    string? bookTitle = Convert.ToString(reader["Title"]);
                    string? bookPublishYear = Convert.ToString(reader["PublishYear"]);
                    int? bookStock = Convert.ToInt32(reader["Stock"]);
                    bool bookActive = Convert.ToBoolean(reader["Active"]);
                    List<Author> authorList = new List<Author>(getBookAuthors((int)bookId));

                    if (bookTitle != null && bookPublishYear != null && bookType != null && publisher != null && bookStock != null)
                    {
                        DateOnly dateOnly = DateOnly.FromDateTime(Convert.ToDateTime(bookPublishYear));

                        Book book = new Book(bookTitle, dateOnly.ToString("dd/MM/yyyy"), bookType, publisher, authorList, (int)bookStock);
                        book.Active = bookActive;
                        book.Id = (int)bookId;
                        books.Add(book);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return books;
        }

        public static List<Book> retriveBooks()
        {
            List<Book> books = new List<Book>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("retriveBooks", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int? bookId = Convert.ToInt32(reader["BookId"]);
                    BookType? bookType = getBookType(Convert.ToInt32(reader["BookTypeId"]));
                    Models.Publisher? publisher = getPublisher(Convert.ToInt32(reader["PublisherId"]));
                    string? bookTitle = Convert.ToString(reader["Title"]);
                    string? bookPublishYear = Convert.ToString(reader["PublishYear"]);
                    int? bookStock = Convert.ToInt32(reader["Stock"]);
                    bool bookActive = Convert.ToBoolean(reader["Active"]);
                    List<Author> authorList = new List<Author>(getBookAuthors((int) bookId));

                    if(bookTitle != null && bookPublishYear != null && bookType != null && publisher != null && bookStock != null) {
                        DateOnly dateOnly = DateOnly.FromDateTime(Convert.ToDateTime(bookPublishYear));

                        Book book = new Book(bookTitle, dateOnly.ToString("dd/MM/yyyy"), bookType, publisher, authorList, (int)bookStock);
                        book.Active = bookActive;
                        book.Id = (int) bookId;
                        books.Add(book);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return books;
        }

        public static List<Author> retriveAuthors()
        {
            List<Author> authors = new List<Author>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("retriveAuthors", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string? AuthorFirstName = Convert.ToString(reader["FirstName"]);
                    string? AuthorLastName = Convert.ToString(reader["LastName"]);
                    string? AuthorBirth = Convert.ToString(reader["BirthDate"]);
                    if (AuthorFirstName != null && AuthorLastName != null && AuthorBirth != null)
                    {
                        DateOnly dateOnly = DateOnly.FromDateTime(Convert.ToDateTime(AuthorBirth));

                        Author author = new Author(AuthorFirstName, AuthorLastName, dateOnly.ToString("dd/MM/yyyy"));

                        author.Id = Convert.ToInt32(reader["AuthorId"]);
                        author.Active = Convert.ToBoolean(reader["Active"]);
                        authors.Add(author);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return authors;
        }

        public static List<Models.Publisher> retrivePublishers()
        {
            List<Models.Publisher> publishers = new List<Models.Publisher>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("retrivePublishers", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string? publisherName = Convert.ToString(reader["Name"]);
                    if (publisherName != null)
                    {
                        Models.Publisher publisher = new Models.Publisher(publisherName);
                        publisher.Id = Convert.ToInt32(reader["PublisherId"]);
                        publisher.Active = Convert.ToBoolean(reader["Active"]);
                        publishers.Add(publisher);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return publishers;
        }

        public static List<BookType> retriveBookTypes()
        {
            List<BookType> bookTypes = new List<BookType>();

            SqlConnection conn = Connection;
            SqlCommand cmd = new SqlCommand("retriveBookTypes", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    string? bookTypeName = Convert.ToString(reader["Name"]);
                    if (bookTypeName != null)
                    {
                        BookType bookType = new BookType(bookTypeName);
                        bookType.Id = Convert.ToInt32(reader["BookTypeId"]);
                        bookType.Active = Convert.ToBoolean(reader["Active"]);
                        bookTypes.Add(bookType);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


            return bookTypes;
        }

        public static bool doesBorrowExists(int UserId, int BookId)
        {
            int count = 0;

            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("doesBorrowExists", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BookId", BookId);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


            return count > 0;
        }

        public static bool doesBookExists(string Title)
        {
            int count = 0;

            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("doesBookExists", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Title", Title);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


            return count > 0;
        }

        public static bool doesAuthorExists(string FirstName, string LastName)
        {
            int count = 0;

            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("doesAuthorExists", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


            return count > 0;
        }

        public static bool doesPublisherExists(string Name)
        {
            int count = 0;

            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("doesPublisherExists", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", Name);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


            return count > 0;
        }

        public static bool doesBookTypeExists(string Name)
        {
            int count = 0;

            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("doesBookTypeExists", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", Name);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


            return count > 0;
        }

        public static bool doesAccountExists(string Email, string Phone)
        {
            int count = 0;

            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("doesAccountExists", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);

            try
            {
                conn.Open();
                count = (int) cmd.ExecuteScalar();
                
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


            return count > 0;
        }

        // private
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnStr"].ConnectionString;
    }
}
