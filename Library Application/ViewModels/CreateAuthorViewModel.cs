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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Library_Application.ViewModels
{
    internal class CreateAuthorViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        // public
        public ICommand AuthorCreate { get; }
        public ICommand CancelCreation { get; }

        public string FirstName
        {
            get => first_name;
            set
            {
                first_name = value;

                ClearErrors(nameof(FirstName));
                if (string.IsNullOrEmpty(first_name))
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
        public string BirthDate
        {
            get => birth_date;
            set
            {
                birth_date = value;

                ClearErrors(nameof(BirthDate));
                if (!Regex.IsMatch(birth_date, @"^(0?[1-9]|[12][0-9]|3[01])\.(0?[1-9]|1[0-2])\.\d{4}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    AddError(nameof(BirthDate), "* You must introduce a date that respects the format!");
                }

                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public bool AuthorAlreadyExists
        {
            get => author_already_exists;
            set
            {
                author_already_exists = value;

                OnPropertyChanged(nameof(AuthorAlreadyExists));
            }
        }

        public bool EmptyFields
        {
            get=> FirstName == string.Empty || LastName == string.Empty || BirthDate == string.Empty;
        }

        public bool HasErrors => property_errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return property_errors.GetValueOrDefault(propertyName, []);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
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

        public CreateAuthorViewModel(Session session, Navigation navigation)
        {
            AuthorCreate = new CreateEntityCommand("author", "create", session, navigation);
            CancelCreation = new CreateEntityCommand("author", "cancel", session, navigation);

            this.session = session;
            this.navigation = navigation;

            first_name = string.Empty;
            last_name = string.Empty;
            birth_date = string.Empty;
            author_already_exists = false;
        }

        // private
        private readonly Session session;
        private readonly Navigation navigation;
        private readonly Dictionary<string, List<string>> property_errors = new Dictionary<string, List<string>>();

        private string first_name;
        private string last_name;
        private string birth_date;
        private bool author_already_exists;

        private void OnErrorsChange(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
