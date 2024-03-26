﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library_Application.Models;

namespace Library_Application.Database
{
    internal class DBUtils
    {
        // public
        public static SqlConnection Connection
        {
            get => new SqlConnection(connectionString);
        }

        public static User? retriveUserData(int Id)
        {
            SqlConnection conn = Connection;

            SqlCommand cmd = new SqlCommand("retriveUserData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);

            User? user = null;

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

                    user = new User(
                            firstName,
                            lastName,
                            email,
                            phone,
                            active,
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
