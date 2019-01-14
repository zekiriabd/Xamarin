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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                lbConnection.FadeTo(0).ContinueWith((x) => { });
            }
            else
            {
                lbConnection.FadeTo(1).ContinueWith((x) => { });
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
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
