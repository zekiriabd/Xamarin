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
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                DisplayAlert("Connection", "Is conetcted", "ok");
            }
            else
            {
                DisplayAlert("Connection", "Is not conetcted", "ok");
            }

            List<ConnectionProfile> profiles = Connectivity.ConnectionProfiles.ToList();
            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                DisplayAlert("WIFI", "Wi-Fi  is Active", "ok");
            }
            else
            {
                DisplayAlert("WIFI", "Wi-Fi is not Active", "ok");
            }


        }
    }
}
