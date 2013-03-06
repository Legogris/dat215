using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable()]
    public class User
    {
        public string Password { private get; set; }
        public string Email { get; set; }
        public ShippingAddress ShippingAddress { get; set; }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
            ShippingAddress = new ShippingAddress();
            ShippingAddress.Email = email;
        }

        public bool isPassword(string password)
        {
            return Password == password;
        }
        public bool passwordExists()
        {
            if (Password != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
