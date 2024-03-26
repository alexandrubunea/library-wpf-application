using Library_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Stores
{
    class Session
    {
        // public
        public User User
        {
            get => user;
        }

        public Session(User user)
        {
            this.user = user;
        }

        // private
        private readonly User user;
    }
}
