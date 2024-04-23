using Library_Application.Commands;
using Library_Application.Models;
using Library_Application.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library_Application.ViewModels
{
    class CreateBookTypeViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        // public
        public ICommand SaveButton { get; }
        public ICommand CancelButton { get; }


        public string Name
        {
            get => name;
            set
            {
                name = value;

                ClearErrors(nameof(Name));
                if (string.IsNullOrEmpty(name))
                {
                    AddError(nameof(Name), "* This field is required.");
                }

                OnPropertyChanged(nameof(Name));
            }
        }

        public bool BookTypeAlreadyExists
        {
            get => book_type_already_exists;
            set
            {
                book_type_already_exists = value;
                OnPropertyChanged(nameof(BookTypeAlreadyExists));
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

        public bool EditMode
        {
            get => edit_mode;
        }

        public BookType BookType
        {
            get => book_type;
            set
            {
                book_type = value;
                Name = book_type.Name;
                BookTypeAlreadyExists = false;
            }
        }

        public CreateBookTypeViewModel(Session session, Navigation navigation, bool edit_mode=false)
        {
            this.session = session;
            this.navigation = navigation;
            this.name = string.Empty;
            this.book_type_already_exists = false;

            SaveButton = new CreateEntityCommand("booktype", "save", session, navigation);
            CancelButton = new CreateEntityCommand("booktype", "cancel", session, navigation);

            this.edit_mode = edit_mode;
            book_type = new BookType(string.Empty);
        }

        // private
        private bool book_type_already_exists;
        private string name;
        private readonly Session session;
        private readonly Navigation navigation;
        private readonly Dictionary<string, List<string>> property_errors = new Dictionary<string, List<string>>();

        private bool edit_mode;
        private BookType book_type;

        private void OnErrorsChange(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
