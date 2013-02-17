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

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public bool isPassword(string password)
        {
            return Password == password;
        }
    }
}
