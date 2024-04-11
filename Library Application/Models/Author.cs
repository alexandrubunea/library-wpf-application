using Library_Application.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Models
{
    internal class Author
    {
        // public
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate {  get; set; }
        public bool Active { get; set; }
        public int NumberOfBooks { get; set; }
        public DateTime BirthDateDate
        {
            get
            {
                return Convert.ToDateTime(BirthDate);
            }
        }

        public Author(string FirstName, string LastName, string BirthDate)
        {
            this.Id = -1;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.BirthDate = BirthDate;
            this.Active = true;
        }

        public void store()
        {
            SqlConnection conn = DBUtils.Connection;

            SqlCommand cmd = new SqlCommand("createAuthor", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Date", DateOnly.FromDateTime(Convert.ToDateTime(BirthDate)));

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

        public void update()
        {
            int bitConvert = Active == true ? 1 : 0;

            SqlConnection conn = DBUtils.Connection;
            SqlCommand cmd = new SqlCommand("updateAuthor", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AuthorId", Id);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@BirthDate", BirthDateDate);
            cmd.Parameters.AddWithValue("@Active", bitConvert);

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

        public void setActiveStatus(bool Active)
        {
            int bitConvert = Active == true ? 1 : 0;

            SqlConnection conn = DBUtils.Connection;
            SqlCommand cmd = new SqlCommand("setAuthorStatus", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Active", bitConvert);


            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                this.Active = Active;
            }
            catch (Exception ex)
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

        public void fetchNumberOfBooks()
        {
            NumberOfBooks = DBUtils.countAuthorBooks(Id);
        }
    }
}
