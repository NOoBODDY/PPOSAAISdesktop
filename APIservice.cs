using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ClientDB
{
    public class APIservice
    {
        static string baseURL = "";
        string username;
        string password;
        string access_token;

        public APIservice(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        
        void LogIn()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseURL);
            request.Method = "POST";
            request.ContentType = "x-www-form-urlencoded";
            string payload = "username{username}&password{password}";
            request.GetRequestStream().Write(Encoding.UTF8.GetBytes(payload), 0, payload.Length);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            access_token = response.Headers["access_token"];
        }

        

    }
}
