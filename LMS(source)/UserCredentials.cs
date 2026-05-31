using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS
{
    public static class UserCredentials
    {
        public static string Username { get; private set; }
        public static string Password { get; private set; }

        public static void SetCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public static void ClearCredentials()
        {
            Username = null;
            Password = null;
        }
    }
}
