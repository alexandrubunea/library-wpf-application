using Library_Application.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Application.ViewModels;

namespace Library_Application.Commands
{
    class ManageBooksCommand : CommandBase
    {
        // public
        public ManageBooksCommand(Session session, Navigation navigation, string button)
        {
            this.session = session;
            this.navigation = navigation;
            this.button = button;
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                return;

            if(button == "create")
            {
                navigation.currentViewModel = new CreateBookViewModel(session, navigation);
                return;
            }
            if (button == "activate")
            {

            }
            if (button == "deactivate")
            {

            }
        }

        // private
        private readonly Session session;
        private readonly Navigation navigation;
        private string button;
    }
}
