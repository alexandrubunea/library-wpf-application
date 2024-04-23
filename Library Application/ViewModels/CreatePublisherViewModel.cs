using Library_Application.Commands;
using Library_Application.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Library_Application.Models;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library_Application.ViewModels
{
    internal class CreatePublisherViewModel : ViewModelBase, INotifyDataErrorInfo
    {
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

        public bool PublisherAlreadyExists
        {
            get => publisher_already_exists;
            set
            {
                publisher_already_exists = value;
                OnPropertyChanged(nameof(PublisherAlreadyExists));
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

        public Publisher Publisher
        {
            get => publisher;
            set
            {
                publisher = value;
                Name = publisher.Name;
                PublisherAlreadyExists = false;
            }
        }

        public bool EditMode
        {
            get => edit_mode;
        }

        public CreatePublisherViewModel(Session session, Navigation navigation, bool edit_mode=false)
        {
            this.session = session;
            this.navigation = navigation;
            this.name = string.Empty;
            this.publisher_already_exists = false;

            SaveButton = new CreateEntityCommand("publisher", "save", session, navigation);
            CancelButton = new CreateEntityCommand("publisher", "cancel", session, navigation);

            this.edit_mode = edit_mode;
            publisher = new Publisher(string.Empty);
        }

        // private
        private bool publisher_already_exists;
        private string name;
        private readonly Session session;
        private readonly Navigation navigation;
        private readonly Dictionary<string, List<string>> property_errors = new Dictionary<string, List<string>>();

        private bool edit_mode;
        private Publisher publisher;

        private void OnErrorsChange(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
