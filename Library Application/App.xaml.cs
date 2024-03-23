using Library_Application.Stores;
using Library_Application.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Library_Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // public
        public App()
        {
            navigation = new Navigation();
        }

        // protected
        protected override void OnStartup(StartupEventArgs e)
        {
            navigation.currentViewModel = new StartViewModel(navigation);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigation)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        // private
        private readonly Navigation navigation;
    }

}
