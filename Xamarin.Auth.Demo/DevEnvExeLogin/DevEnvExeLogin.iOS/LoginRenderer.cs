using System;
using FacebookLogin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using Xamarin.Auth;
using Newtonsoft.Json;
using DevEnvExeLogin;
using DevEnvExeLogin.iOS.PageRender;




[assembly: ExportRenderer(typeof(ProviderLoginPage), typeof(LoginRenderer))]

namespace DevEnvExeLogin.iOS.PageRender
{
    public class LoginRenderer : PageRenderer
    {
        public LoginRenderer() { }

        private bool showLogin = true;
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (showLogin && FacebookAuth.User == null)
            {
                showLogin = false;
               
                var auth = FacebookAuth.FacebookAuthByClientId();

                auth.Completed += async (sender, eventArgs) =>
                {
                    DismissViewController(true, null);

                    if (eventArgs.IsAuthenticated)
                    {

                        var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=name,picture,cover,birthday"), null, eventArgs.Account);
                        var fbResponse = await request.GetResponseAsync();
                        FacebookAuth.User = JsonConvert.DeserializeObject<UserM>(fbResponse.GetResponseText());
                        FacebookAuth.SuccessfulLoginAction.Invoke();
                       
                    }
                    else
                    {
                            // The user cancelled
                    }
                    
                };

                PresentViewController(auth.GetUI(), true, null);
            }
        }
    }

}