using Library_Application.Commands;
using Library_Application.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library_Application.ViewModels
{
    internal class RegisterViewModel : ViewModelBase, IDataErrorInfo
    {
        // public
        public ICommand RegisterCommand { get; }
        public ICommand BackCommand { get; }

        public string FirstName
        {
            get
            {
                return first_name;
            }
            set
            {
                first_name = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get
            {
                return last_name;
            }
            set
            {
                last_name = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return confirm_password;
            }
            set
            {
                confirm_password = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string Error => "Something went wrong.";

        public string this[string columnName]
        {
            get
            {
                if(columnName == "FirstName")
                {
                    if (string.IsNullOrWhiteSpace(FirstName))
                        return "* Field must be completed.";
                }

                if(columnName == "LastName")
                {
                    if (string.IsNullOrWhiteSpace(LastName))
                        return "* Field must be completed.";
                }

                if(columnName == "Email")
                {
                    if (string.IsNullOrWhiteSpace(Email))
                        return "* Field must be completed.";
                    if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                        return "* This is not a valid email address.";
                }

                if(columnName == "Phone")
                {
                    if (string.IsNullOrWhiteSpace(Phone))
                        return "* Field must be completed.";
                    if (!Regex.IsMatch(Phone, @"^\+?[0-9]{10,15}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                        return "* This is not a valid phone number.";
                }

                if(columnName == "Password")
                {
                    if (string.IsNullOrWhiteSpace(Password))
                        return "* Field must be completed.";
                    if (!Regex.IsMatch(Password, @"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{5,}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                        return "* This password is too weak. Try to add special characters/numbers.";
                    if (!string.IsNullOrWhiteSpace(ConfirmPassword) && Password != ConfirmPassword)
                        return "* The passwords does not match.";
                }

                if(columnName == "ConfirmPassword")
                {
                    if (string.IsNullOrWhiteSpace(ConfirmPassword))
                        return "* Field must be completed.";
                    if (!Regex.IsMatch(Password, @"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{5,}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                        return "* This password is too weak. Try to add special characters/numbers.";
                    if (!string.IsNullOrWhiteSpace(Password) && ConfirmPassword != Password)
                        return "* The passwords does not match.";
                }

                return "";
            }
        }

        public RegisterViewModel(Navigation navigation)
        {
            RegisterCommand = new RegisterCommand("register", navigation, valid_data);
            BackCommand = new RegisterCommand("back", navigation, valid_data);

            first_name = "";
            last_name = "";
            email = "";
            phone = "";
            password = "";
            confirm_password = "";
            
            valid_data = false;
        }

        // private
        private string first_name;
        private string last_name;
        private string email;
        private string phone;
        private string password;
        private string confirm_password;

        private bool valid_data;
    }
}
