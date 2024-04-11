using Library_Application.Commands;
using Library_Application.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library_Application.ViewModels
{
    internal class EditPublisherViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        public ICommand PublisherEdit { get; }
        public ICommand CancelEdit { get; }


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

        public Models.Publisher Publisher
        {
            get => publisher;
            set
            {
                publisher = value;
                OnPropertyChanged(nameof(Publisher));
            }
        }

        public EditPublisherViewModel(Session session, Navigation navigation, Models.Publisher publisher)
        {
            this.session = session;
            this.navigation = navigation;
            this.publisher = publisher;
            this.name = publisher.Name;
            this.publisher_already_exists = false;

            PublisherEdit = new EditEntityCommand("publisher", "edit", session, navigation);
            CancelEdit = new EditEntityCommand("publisher", "cancel", session, navigation);
        }

        // private
        private bool publisher_already_exists;
        private string name;
        private readonly Session session;
        private readonly Navigation navigation;
        private readonly Dictionary<string, List<string>> property_errors = new Dictionary<string, List<string>>();
        private Models.Publisher publisher;

        private void OnErrorsChange(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
