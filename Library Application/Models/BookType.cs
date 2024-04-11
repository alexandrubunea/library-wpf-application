using Library_Application.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Library_Application.Models
{
    internal class BookType
    {
        // public
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int NumberOfBooks { get; set; }

        public BookType(string Name)
        {
            this.Name = Name;
            this.Active = true;
        }

        public void store()
        {
            SqlConnection conn = DBUtils.Connection;
            SqlCommand cmd = new SqlCommand("createBookType", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", Name);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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
        }

        public void update()
        {
            int bitConvert = Active == true ? 1 : 0;

            SqlConnection conn = DBUtils.Connection;
            SqlCommand cmd = new SqlCommand("updateBookType", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookTypeId", Id);
            cmd.Parameters.AddWithValue("@Name", Name);
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
            SqlCommand cmd = new SqlCommand("setBookTypeActiveStatus", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Active", bitConvert);


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

        public void fetchNumberOfBooks()
        {
            NumberOfBooks = DBUtils.countBookTypeBooks(Id);
        }

        // private
    }
}
