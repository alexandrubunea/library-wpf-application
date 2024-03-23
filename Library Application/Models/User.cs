using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Models
{
    internal class User
    {
        // public
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public int AccessLevel { get; set; }
        
        public User(int Id, string FirstName, string LastName, string Email, string Phone, bool Active, int AccessLevel)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Active = Active;
            this.AccessLevel = AccessLevel;
        }

        public void updatePassword(string newPassword)
        {

        }

        public void store()
        {

        }
        
        public void update()
        {

        }
        // private
    }
}
