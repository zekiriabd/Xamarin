using FacebookLogin.Models;
using Newtonsoft.Json;
using System;
using Xamarin.Auth;
using Xamarin.Forms;

namespace DevEnvExeLogin
{
    public static class FacebookAuth
    {
        public static Object User;
        public static void SetUser(string Provider, string response)
        {
            switch (Provider)
            {
                case "Google": User =  JsonConvert.DeserializeObject<GUserM>(response.Replace("\n", "").Replace("\"", "'"));  break;
                case "FaceBook": User = JsonConvert.DeserializeObject<FUserM>(response); break;
            }

            Application.Current.MainPage = new HomePage();
        }

        
    }
}
