using System;
using FacebookLogin.Models;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Xaml;
using Xamarin.Forms;

using Xamarin.Auth;
using Newtonsoft.Json;
using DevEnvExeLogin;
using DevEnvExeLogin.UWP.PageRender;


[assembly: ExportRenderer(typeof(ProviderLoginPage), typeof(LoginRenderer))]

namespace DevEnvExeLogin.UWP.PageRender
{
    public class LoginRenderer : PageRenderer
    {
        //public LoginRenderer() { }
        private Windows.UI.Xaml.Controls.Frame _frame;
        private bool showLogin = true;


        private OAuth2Authenticator GetAuthenticator(string Provider)
        {
            OAuth2Authenticator auth = null;
            switch (Provider)
            {
                case "Google":
                    {
                        auth = new OAuth2Authenticator
                    (
                      "912800401855-34ilavblagsrr6je46lsj5fbpbl2831i.apps.googleusercontent.com",
                      "https://www.googleapis.com/auth/userinfo.email",
                      new Uri("https://accounts.google.com/o/oauth2/auth"),
                      new Uri("https://www.youtube.com/channel/UCIOFNKDimJlBKQ2xp15CbMA?view_as=subscriber"),
                      isUsingNativeUI: true
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
            
                if (showLogin && FacebookAuth.User == null)
                {
                    showLogin = false;
                    var loginPage = Element as ProviderLoginPage;
                    string providername = loginPage.ProviderName;

                    WindowsPage windowsPage = new WindowsPage();
                    var auth = GetAuthenticator(providername);
                    _frame = windowsPage.Frame;
                    if (_frame == null)
                    {
                        _frame = new Windows.UI.Xaml.Controls.Frame();
                        //_frame.Language = global::Windows.Globalization.ApplicationLanguages.Languages[0];
                        windowsPage.Content = _frame;
                        SetNativeControl(windowsPage);
                    }

                    auth.Completed += async (sender, eventArgs) => {

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

                    
                    _frame.Navigate(auth.GetUI(), auth);
                    Window.Current.Activate();
                }
            
        }
    }
}