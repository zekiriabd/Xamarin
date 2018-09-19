using System;
using Xamarin.Forms;

namespace DevEnvExeLogin
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
   
        private void Login_Clicked(object sender, EventArgs e)
        {
             Navigation.PushModalAsync(new ProviderLoginPage(((Button)sender).Text));
        }
    }
}
