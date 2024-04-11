using Library_Application.Database;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Models
{
    internal class UserBook
    {
        public int Id { get; set; }
        public RestrictedDataUser User { get; set; }
        public Book Book { get; set; }
        public string StartDate { get; set; }
        public string ReturnDate { get; set; }
        public bool Active { get; set; }
        public DateTime DateForOrgz
        {
            get
            {
                return Convert.ToDateTime(ReturnDate);
            }
        }

        public UserBook(RestrictedDataUser user, Book book, string startDate, string returnDate)
        {
            Id = -1;
            User = user;
            Book = book;
            StartDate = startDate;
            ReturnDate = returnDate;
        }

        public void store()
        {
            SqlConnection conn = DBUtils.Connection;

            SqlCommand cmd = new SqlCommand("createUserBook", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", User.Id);
            cmd.Parameters.AddWithValue("@BookId", Book.Id);
            cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(StartDate));
            cmd.Parameters.AddWithValue("@ReturnDate", Convert.ToDateTime(ReturnDate));

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
            SqlCommand cmd = new SqlCommand("setBorrowActiveStatus", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BorrowId", Id);
            cmd.Parameters.AddWithValue("@Status", bitConvert);

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
    }
}
