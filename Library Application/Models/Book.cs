using Library_Application.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Models
{
    internal class Book
    {
        // public
        public int Id { get; set; }
        public string Title { get; set; }
        public string PublishYear { get; set; }
        public BookType BookType { get; set; }
        public Publisher Publisher { get; set; }
        public List<Author> Authors { get; set; }
        public bool Active { get; set; }
        public int Stock { get; set; }
        public DateTime PublishDate
        {
            get
            {
                return Convert.ToDateTime(PublishYear);
            }
        }

        public Book(string Title, string PublishYear, BookType BookType, Publisher Publisher, List<Author> Authors, int Stock)
        {
            this.Id = -1;
            this.Title = Title;
            this.PublishYear = PublishYear;
            this.BookType = BookType;
            this.Publisher = Publisher;
            this.Authors = Authors;
            this.Active = true;
            this.Stock = Stock;
        }

        public void update()
        {
            int bitConvert = Active == true ? 1 : 0;

            SqlConnection conn = DBUtils.Connection;
            SqlCommand cmd = new SqlCommand("updateBook", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookId", Id);
            cmd.Parameters.AddWithValue("@BookTypeId", BookType.Id);
            cmd.Parameters.AddWithValue("@PublisherId", Publisher.Id);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@PublishYear", PublishDate);
            cmd.Parameters.AddWithValue("@Stock", Stock);
            cmd.Parameters.AddWithValue("@Active", bitConvert);

            SqlCommand cmd_createAuthorBook = new SqlCommand("createAuthorBook", conn);
            cmd_createAuthorBook.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                
                if (!areAuthorsEqual(Authors, DBUtils.getBookAuthors(Id)))
                {
                    DBUtils.removeAuthorsForBook(Id);

                    for (int i = 0; i < Authors.Count; i++)
                    {
                        cmd_createAuthorBook.Parameters.AddWithValue("@AuthorId", Authors[i].Id);
                        cmd_createAuthorBook.Parameters.AddWithValue("@BookId", Id);
                        cmd_createAuthorBook.Parameters.AddWithValue("@NumberInList", i);

                        cmd_createAuthorBook.ExecuteNonQuery();
                        cmd_createAuthorBook.Parameters.Clear();
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
        }

        public void store()
        {
            if (Id == -1)
                throw new Exception("Invalid book id, cannot continue!");

            SqlConnection conn = DBUtils.Connection;

            SqlCommand cmd_createBook = new SqlCommand("createBook", conn);
            cmd_createBook.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd_createAuthorBook = new SqlCommand("createAuthorBook", conn);
            cmd_createAuthorBook.CommandType = CommandType.StoredProcedure;

            cmd_createBook.Parameters.AddWithValue("@Title", Title);
            cmd_createBook.Parameters.AddWithValue("@BookTypeId", BookType.Id);
            cmd_createBook.Parameters.AddWithValue("@PublisherId", Publisher.Id);
            cmd_createBook.Parameters.AddWithValue("@PublishYear", DateOnly.FromDateTime(Convert.ToDateTime(PublishYear)));
            cmd_createBook.Parameters.AddWithValue("@Stock", Stock);

            try
            {
                conn.Open();
                cmd_createBook.ExecuteNonQuery();

                for(int i = 0; i < Authors.Count; i++)
                {
                    cmd_createAuthorBook.Parameters.AddWithValue("@AuthorId", Authors[i].Id);
                    cmd_createAuthorBook.Parameters.AddWithValue("@BookId", Id);
                    cmd_createAuthorBook.Parameters.AddWithValue("@NumberInList", i);

                    cmd_createAuthorBook.ExecuteNonQuery();
                    cmd_createAuthorBook.Parameters.Clear();
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
        }

        public void setActiveStatus(bool Active)
        {
            int bitConvert = Active == true ? 1 : 0;

            SqlConnection conn = DBUtils.Connection;
            SqlCommand cmd = new SqlCommand("setBookActiveStatus", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookId", Id);
            cmd.Parameters.AddWithValue("@Status", bitConvert);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                this.Active = Active;
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

        // private
        private bool areAuthorsEqual(List<Author> list1, List<Author> list2)
        {
            if(list1.Count != list2.Count) 
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (list1[i].Id != list2[i].Id)
                    return false;


            return true;
        }
    }
}
