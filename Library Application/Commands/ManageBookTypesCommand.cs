using Library_Application.Stores;
using Library_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Library_Application.Commands
{
    class ManageBookTypesCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if(button == "create")
            {
                navigation.currentViewModel = new CreateBookTypeViewModel(session, navigation);
            }
        }

        public ManageBookTypesCommand(string button, Session session, Navigation navigation)
        {
            this.button = button;
            this.session = session;
            this.navigation = navigation;
        }

        // private
        private readonly string button;
        private readonly Session session;
        private readonly Navigation navigation;

    }
}
