using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace BarCodeScanner
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var scanBarObject = new ZXingScannerPage();
            await Navigation.PushAsync(scanBarObject);
            scanBarObject.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    CodeBarText.Text = result.Text;
                });
            };
        }
    }
}
