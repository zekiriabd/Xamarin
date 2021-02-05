using DevEnvExeLogin;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;
using Xamarin.Forms;
namespace XamarinAuth
{
    public class Oauth
    {
        public static OAuth2Authenticator auth = null;
        public static Action SuccessfulLoginAction
        {
            get { return () => App._nav?.Navigation?.PopModalAsync(); }
        }
       
        public static void CreateOAuth(string Provider)
        {
            try
            {
                switch (Provider)
                {
                    case "Google":
                        auth = GetOauthGoogle();
                        break;
                    default: //Case "Facebook"
                        auth = GetOauthFacebook();
                        break;

                }

                auth.Completed += async (sender, e) =>
                {
                    if (e.IsAuthenticated)
                    {
                        OAuth2Request request = null;
                        switch (Provider)
                        {
                            case "Google": request = new OAuth2Request("GET", new Uri("https://www.googleapis.com/oauth2/v2/userinfo"), null, null); break;
                            default: request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=name,picture,cover,birthday"), null, null); break;
                        }
                        request.Account = e.Account;
                        var fbResponse = await request?.GetResponseAsync();
                        FacebookAuth.SetUser(Provider, fbResponse.GetResponseText());
                    }
                    else
                    {
                        // The user cancelled
                    }
                };

                var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                presenter.Login(auth);
            }
            catch(Exception ex)
            {
                App._nav.DisplayAlert("Exception",ex.Message,"OK");
            }
        }

        private static OAuth2Authenticator GetOauthFacebook()
        {
            return new OAuth2Authenticator(
                    "265102187457658",
                    "",
                    new Uri("https://m.facebook.com/dialog/oauth/"),
                    new Uri("https://www.facebook.com/connect/login_success.html"),
                    null,
                    true);
        }

        private static OAuth2Authenticator GetOauthGoogle()
        {
            return new OAuth2Authenticator(
                    "912800401855-eb2ohn99h2nf0d0ck7tlq7b3vjd2hqgq.apps.googleusercontent.com",
                    null,
                    "https://www.googleapis.com/auth/userinfo.email",
                    new Uri("https://accounts.google.com/o/oauth2/auth"),
                    new Uri("com.googleusercontent.apps.912800401855-eb2ohn99h2nf0d0ck7tlq7b3vjd2hqgq:/oauth2redirect"),
                    new Uri("https://www.googleapis.com/oauth2/v4/token"),
                    null,
                    true);
        }
    }    
}
