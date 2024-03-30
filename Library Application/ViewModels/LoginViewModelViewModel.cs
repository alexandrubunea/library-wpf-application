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
    internal class LoginViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        // public
        public ICommand LoginCommand { get; }
        public ICommand BackCommand { get; }

        public string Email
        {
            get => email;
            set
            {
                email = value;

                ClearErrors(nameof(Email));

                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    AddError(nameof(Email), "* A valid email address is required.");
                }

                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;

                ClearErrors(nameof(Password));
                if (string.IsNullOrEmpty(password))
                {
                    AddError(nameof(Password), "* This field is required.");
                }

                OnPropertyChanged(nameof(Password));
            }
        }

        public bool AccountDoesntExists
        {
            get => account_doesnt_exists;
            set
            {
                account_doesnt_exists = value;
                OnPropertyChanged(nameof(AccountDoesntExists));
            }
        }

        public bool IncorrectPassword
        {
            get => incorrect_password;
            set
            {
                incorrect_password = value;
                OnPropertyChanged(nameof(IncorrectPassword));
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
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                { return false; }
                return true;
            }
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if (!property_errors.ContainsKey(propertyName))
            {
                property_errors.Add(propertyName, new List<string>());
            }

            property_errors[propertyName].Add(errorMessage);
            OnErrorsChange(propertyName);
        }

        public void ClearErrors(string propertyName)
        {
            if (property_errors.Remove(propertyName))
            {
                OnErrorsChange(propertyName);
            }
        }

        public LoginViewModel(Navigation navigation)
        {
            LoginCommand = new LoginCommand("login", navigation);
            BackCommand = new LoginCommand("back", navigation);

            email = string.Empty;
            password = string.Empty;
            incorrect_password = false;
            account_doesnt_exists = false;
        }

        // private
        private string email;
        private string password;
        private bool incorrect_password;
        private bool account_doesnt_exists;
        private Dictionary<string, List<string>> property_errors = new Dictionary<string, List<string>>();

        private void OnErrorsChange(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
