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
    internal class RegisterViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        // public
        public ICommand RegisterCommand { get; }
        public ICommand BackCommand { get; }

        public string FirstName
        {
            get => first_name;
            set
            {
                first_name = value;

                ClearErrors(nameof(FirstName));
                if(string.IsNullOrEmpty(first_name))
                {
                    AddError(nameof(FirstName), "* This field is required.");
                }

                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => last_name;
            set
            {
                last_name = value;

                ClearErrors(nameof(LastName));
                if (string.IsNullOrEmpty(last_name))
                {
                    AddError(nameof(LastName), "* This field is required.");
                }

                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;

                ClearErrors(nameof(Email));
                
                if(!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    AddError(nameof(Email), "* A valid email address is required.");
                }

                OnPropertyChanged(nameof(Email));
            }
        }
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;

                ClearErrors(nameof(Phone));

                if (!Regex.IsMatch(phone, @"^\+?[0-9]{10,15}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    AddError(nameof(Phone), "* A valid phone number is required.");
                }

                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;

                ClearErrors(nameof(Password));
                ClearErrors(nameof(ConfirmPassword));

                if (!Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{5,}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    AddError(nameof(Password), "* The password is too weak. Try to add numbers/special characters.");
                }

                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => confirm_password;
            set
            {
                confirm_password = value;

                ClearErrors(nameof(ConfirmPassword));

                if (!string.Equals(password, confirm_password))
                {
                    AddError(nameof(ConfirmPassword), "* The passwords don't match.");
                }

                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public bool AccountAlreadyExists
        {
            get => account_already_exists;
            set
            {
                account_already_exists = value;
                OnPropertyChanged(nameof(AccountAlreadyExists));
            }
        }

        public bool HasErrors => property_errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return property_errors.GetValueOrDefault(propertyName, []);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }

        public bool NotEmptyData
        {
            get
            {
                if (string.IsNullOrEmpty(first_name) || string.IsNullOrEmpty(last_name) ||
                    string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) ||
                    string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm_password))
                { return false; }
                return true;
            }
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if(!property_errors.ContainsKey(propertyName))
            {
                property_errors.Add(propertyName, new List<string>());
            }

            property_errors[propertyName].Add(errorMessage);
            OnErrorsChange(propertyName);
        }

        public void ClearErrors(string propertyName)
        {
            if(property_errors.Remove(propertyName))
            {
                OnErrorsChange(propertyName);
            }
        }

        public RegisterViewModel(Navigation navigation)
        {
            RegisterCommand = new RegisterCommand("register", navigation);
            BackCommand = new RegisterCommand("back", navigation);

            first_name = string.Empty;
            last_name = string.Empty;
            email = string.Empty;
            phone = string.Empty;
            password = string.Empty;
            confirm_password = string.Empty;
            account_already_exists = false;
        }

        // private
        private string first_name;
        private string last_name;
        private string email;
        private string phone;
        private string password;
        private string confirm_password;
        private bool account_already_exists;
        private readonly Dictionary<string, List<string>> property_errors = new Dictionary<string, List<string>>();

        private void OnErrorsChange(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
