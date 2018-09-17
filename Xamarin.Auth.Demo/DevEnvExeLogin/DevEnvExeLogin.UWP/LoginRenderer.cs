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

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            
                if (showLogin && FacebookAuth.User == null)
                {
                    showLogin = false;
                    WindowsPage windowsPage = new WindowsPage();
                    var auth = FacebookAuth.FacebookAuthByClientId();
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

                    
                    _frame.Navigate(auth.GetUI(), auth);
                    Window.Current.Activate();
                }
            
        }
    }
}