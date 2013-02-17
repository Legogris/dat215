using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class User
    {
        private string password;
        public string UserName { get; private set; }

        public User(string userName, string password)
        {
            UserName = userName;
            this.password = password;
        }

        public bool isPassword(string password)
        {
            return this.password == password;
        }
    }
}
