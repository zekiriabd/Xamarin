using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DevEnvExeLogin.Droid
{
    [Activity(Label = "GoogleAuthenticationActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTask)]
    [
           IntentFilter
           (
               actions: new[] { Intent.ActionView },
               Categories = new[]
               {
                    Intent.CategoryDefault,
                    Intent.CategoryBrowsable
               },
               DataSchemes = new[]
               {
                // First part of the redirect url (Package name)
                "com.googleusercontent.apps.912800401855-eb2ohn99h2nf0d0ck7tlq7b3vjd2hqgq",
               },
               DataPaths = new[]
               {
                // Second part of the redirect url (Path)
                "/oauth2redirect"
               }
           )
       ]
    class GoogleAuthenticationActivity :Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            try
            {
                base.OnCreate(savedInstanceState);

                // Convert Android.Net.Url to Uri
                var uri = new Uri(Intent.Data.ToString());

                new Task(() =>
                {
                    var intent = new Intent(ApplicationContext, typeof(MainActivity));
                    intent.AddFlags(ActivityFlags.IncludeStoppedPackages);
                    intent.AddFlags(ActivityFlags.ReorderToFront);
                    StartActivity(intent);

                }).Start();

                // Load redirectUrl page
                //AuthenticationState.Authenticator.OnPageLoading(uri);
                XamarinAuth.Oauth.auth?.OnPageLoading(uri);
                Finish();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
