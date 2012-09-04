using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8TracksEngine.Model
{
    public class LoginResponse
    {
        public string user_token;
        public string auth_token;
        public User current_user;
        public string notices;
        public string errors;
        public string status;
        public bool logged_in;
    }
}
