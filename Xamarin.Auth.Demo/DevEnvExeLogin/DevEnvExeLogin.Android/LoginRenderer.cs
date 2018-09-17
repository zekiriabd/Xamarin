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
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var activity = this.Context as Activity;

            if (showLogin && FacebookAuth.User == null)
            {
                showLogin = false;
               
                var auth = FacebookAuth.FacebookAuthByClientId();

                auth.Completed += async (sender, eventArgs) =>
                {
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
                activity.StartActivity(auth.GetUI(activity));
            }
        }
    }

}