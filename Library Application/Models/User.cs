using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Application.Database;
using Library_Application.Utils;
using System.Net.Http.Headers;
using Library_Application.Stores;
using Microsoft.VisualBasic.ApplicationServices;

namespace Library_Application.Models
{
    internal class User
    {
        // public
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public int AccessLevel { get; set; }
        
        public User(string FirstName, string LastName, string Email, string Phone, int AccessLevel)
        {
            this.Id = -1;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Active = true;
            this.AccessLevel = AccessLevel;
        }

        public void fetchId()
        {
            if(this.Id != -1)
            {
                return;
            }

            SqlConnection conn = DBUtils.Connection;

            SqlCommand cmd = new SqlCommand("fetchId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);

            int? userId = null;

            try
            {
                conn.Open();
                userId = (int?)cmd.ExecuteScalar();
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

            if (userId != null)
            {
                Id = (int) userId;
            }
        }

        public void updatePassword(string newPassword)
        {
            SqlConnection conn = DBUtils.Connection;

            SqlCommand cmd = new SqlCommand("updatePassword", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Hashing.GetHashString(newPassword));

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

        public void store()
        {
            SqlConnection conn = DBUtils.Connection;

            SqlCommand cmd = new SqlCommand("createUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Password", "default");

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
                if(conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        
        public void update()
        {

        }
        // private
    }
}
