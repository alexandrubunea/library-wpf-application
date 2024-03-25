using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Application.Database;

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
        
        public User(string FirstName, string LastName, string Email, string Phone, bool Active, int AccessLevel)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Active = Active;
            this.AccessLevel = AccessLevel;
        }

        public void updatePassword(string newPassword)
        {

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
            catch
            {

            }
        }
        
        public void update()
        {

        }
        // private
    }
}
