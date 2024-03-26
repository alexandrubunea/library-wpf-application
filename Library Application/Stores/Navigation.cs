using Library_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Stores
{
    internal class Navigation
    {
        // public
        public ViewModelBase? currentViewModel
        {
            get => _currentViewModel;

            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action? CurrentViewModelChanged;

        // private
        private ViewModelBase? _currentViewModel;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
