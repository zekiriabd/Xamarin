using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConnectivityCheck
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            
        }

        private void BtnTestNet_Clicked(object sender, EventArgs e)
        {
            //var current = Connectivity.NetworkAccess;
            //if (current == NetworkAccess.Internet)
            //{
            //    DisplayAlert("test", "Connection to internet is available", "ok");
            //}

            var profiles = Connectivity.ConnectionProfiles;
            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                DisplayAlert("test", "Active Wi-Fi connection", "ok");
            }
        }
    }
}
