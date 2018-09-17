using FacebookLogin.Models;
using System;
using Xamarin.Auth;
using Xamarin.Forms;

namespace DevEnvExeLogin
{
   public static class FacebookAuth
    {
        public static UserM User;
        public static Action SuccessfulLoginAction
        {
            get
            {
                return new Action(() =>
                {
                    Application.Current.MainPage = new HomePage();
                });
            }
        }

        public static OAuth2Authenticator FacebookAuthByClientId()
        {
            return new OAuth2Authenticator(
                 clientId: "265102187457658",
                 scope: "",
                 authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                 redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html")
            );
        }
    }
}
