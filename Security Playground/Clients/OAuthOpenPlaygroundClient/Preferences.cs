using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthOpenPlaygroundClient
{
    public class Preferences
    {
        public static string AuthorizationEndpoint = "http://localhost:5001/connect/authorize";
        public static string TokenEndpoint = "http://localhost:5001/connect/token";
        public static string UserInfoEndpoint = "http://localhost:5001/connect/userinfo";
    }
}
