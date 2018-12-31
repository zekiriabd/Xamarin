using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FloatingAction
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await ((Frame)sender).ScaleTo(0, 50, Easing.Linear);
            await Task.Delay(100);
            await ((Frame)sender).ScaleTo(1, 50, Easing.Linear);

            FloatMenuItem1.IsVisible = true;
            await FloatMenuItem1.TranslateTo(0,0, 100);
            await FloatMenuItem1.TranslateTo(0,-20, 100);
            await FloatMenuItem1.TranslateTo(0,0, 200);

            FloatMenuItem2.IsVisible = true;
            await FloatMenuItem2.TranslateTo(0, 0, 100);
            await FloatMenuItem2.TranslateTo(0, -20, 100);
            await FloatMenuItem2.TranslateTo(0, 0, 200);

            FloatMenuItem3.IsVisible = true;
            await FloatMenuItem3.TranslateTo(0, 0, 100);
            await FloatMenuItem3.TranslateTo(0, -20, 100);
            await FloatMenuItem3.TranslateTo(0, 0, 200);

            

        }
    }
}
