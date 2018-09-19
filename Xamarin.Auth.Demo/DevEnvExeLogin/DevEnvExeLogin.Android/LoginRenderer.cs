using Android.App;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System;
using FacebookLogin.Models;

using Xamarin.Auth;
using Newtonsoft.Json;
using DevEnvExeLogin.Droid.PageRender;
using DevEnvExeLogin;

[assembly: ExportRenderer(typeof(ProviderLoginPage), typeof(LoginRenderer))]

namespace DevEnvExeLogin.Droid.PageRender
{
    public class LoginRenderer : PageRenderer
    {
        public LoginRenderer() { }

        private bool showLogin = true;

        private OAuth2Authenticator GetAuthenticator(string Provider)
        {
            OAuth2Authenticator auth = null;
            switch (Provider)
            {
                case "Google":
                    {

                        auth = new OAuth2Authenticator(
                            "912800401855-eb2ohn99h2nf0d0ck7tlq7b3vjd2hqgq.apps.googleusercontent.com",
                            "https://www.googleapis.com/auth/userinfo.email",
                            new Uri("https://accounts.google.com/o/oauth2/auth"),
                            new Uri("https://www.youtube.com/channel/UCIOFNKDimJlBKQ2xp15CbMA?view_as=subscriber")
                        ); 

                        break;
                    }
                case "FaceBook":
                    {
                        auth = new OAuth2Authenticator(
                                clientId: "265102187457658",
                                scope: "",
                                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                                redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html")
                        );
                        break;
                    }
            }
            return auth;
        }

        public OAuth2Request GetRequest(string Provider)
        {
            OAuth2Request request = null;
            switch (Provider)
            {
                case "Google": request = new OAuth2Request("GET", new Uri("https://www.googleapis.com/oauth2/v2/userinfo"), null, null); break;
                case "FaceBook": request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=name,picture,cover,birthday"), null, null); break;
            }
            return request;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            var activity = this.Context as Activity;
            if (showLogin && FacebookAuth.User == null)
            {
                showLogin = false;
                var loginPage = Element as ProviderLoginPage;
                string providername = loginPage.ProviderName;
                var auth = GetAuthenticator(providername);

                auth.Completed += async (sender, eventArgs) =>
                {
                    if (eventArgs.IsAuthenticated)
                    {
                        var request = GetRequest(providername);
                        request.Account = eventArgs.Account;
                        var fbResponse = await request.GetResponseAsync();
                        FacebookAuth.SetUser(providername, fbResponse.GetResponseText());
                    }
                    else
                    {
                        // The user cancelled
                    }
                };
                activity.StartActivity(auth.GetUI(activity));
            }
        }
    }

}