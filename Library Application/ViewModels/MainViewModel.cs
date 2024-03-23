using Library_Application.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library_Application.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        // public
        public ViewModelBase? CurrentViewModel => navigation.currentViewModel;

        public MainViewModel(Navigation navigation)
        {
            this.navigation = navigation;
            this.navigation.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        // private
        private readonly Navigation navigation;

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
