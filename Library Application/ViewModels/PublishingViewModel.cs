using Library_Application.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.ViewModels
{
    class PublishingViewModel : ViewModelBaseSideBar
    {
        public PublishingViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
        }
    }
}
