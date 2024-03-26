using Library_Application.Database;
using Library_Application.Stores;
using Library_Application.ViewModels;
using Microsoft.Data.SqlClient;
using Library_Application.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Application.Models;

namespace Library_Application.Commands
{
    internal class LoginCommand : CommandBase
    {
        // public

        public LoginCommand(string buttonName, Navigation navigation)
        {
            this.buttonName = buttonName;
            this.navigation = navigation;
        }

        public override void Execute(object? parameter)
        {
            LoginViewModel? viewModel = navigation.currentViewModel as LoginViewModel;
            if (buttonName == "login" && viewModel.NotEmptyData && !viewModel.HasErrors)
            {
                viewModel.IncorrectPassword = false;
                viewModel.AccountDoesntExists = false;

                if(!DBUtils.doesAccountExists(viewModel.Email, " "))
                {
                    viewModel.AccountDoesntExists = true;
                    return;
                }

                SqlConnection conn = DBUtils.Connection;
                SqlCommand cmd = new SqlCommand("checkCredentials", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", viewModel.Email);
                cmd.Parameters.AddWithValue("@Password", Hashing.GetHashString(viewModel.Password));

                int? userId = null;

                try
                {
                    conn.Open();
                    userId = (int?) cmd.ExecuteScalar();
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

                if (userId == null)
                {
                    viewModel.IncorrectPassword = true;
                    return;
                }

                User? userData = DBUtils.retriveUserData((int) userId);
                if(userData != null)
                {
                    userData.fetchId();
                    Session session = new Session(userData);

                    navigation.currentViewModel = new AllBooksViewModel(session, navigation);
                }
            }
            else if(buttonName == "back")
            {
                navigation.currentViewModel = new StartViewModel(navigation);
            }
        }

        // private

        private string buttonName;
        private Navigation navigation;
    }
}
