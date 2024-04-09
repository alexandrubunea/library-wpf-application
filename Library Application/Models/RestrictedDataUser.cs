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
    internal class RestrictedDataUser
    {
        // public
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }

        public RestrictedDataUser(int id, string firstName, string lastName, string email, string phone, bool active)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Active = active;
        }

        public void setActiveStatus(bool Active)
        {
            int bitConvert = Active == true ? 1 : 0;

            SqlConnection conn = DBUtils.Connection;
            SqlCommand cmd = new SqlCommand("setUserActiveStatus", conn);
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
        // static methods
        public static RestrictedDataUser ConvertUserToRDUser(User user)
        {
            RestrictedDataUser result = new RestrictedDataUser(user.Id, user.FirstName, user.LastName, user.Email, user.Phone, user.Active);
            return result;
        }
        // private
    }
}
