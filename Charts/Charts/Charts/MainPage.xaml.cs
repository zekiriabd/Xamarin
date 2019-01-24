
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Entry = Microcharts.Entry;
using Microcharts;
using SkiaSharp;
using Newtonsoft.Json;


namespace Charts
{
    public partial class MainPage : ContentPage
    {
        List<Entry> entries = new List<Entry>{
            new Entry(200)
            {
                Label = "January",
                ValueLabel = "200",
                Color = SKColor.Parse("#266489")
            },
            new Entry(400)
            {
                Label = "February",
                ValueLabel = "400",
                Color = SKColor.Parse("#68B9C0")
            },
            new Entry(-100)
            {
                Label = "March",
                ValueLabel = "-100",
                Color = SKColor.Parse("#90D585")
            }
        };


        public MainPage()
        {
            InitializeComponent();
            //chart1.Chart = new BarChart() { Entries = entries };

            //chart1.Chart = new PointChart() { Entries = entries };
            //chart1.Chart = new LineChart() { Entries = entries };
            //chart1.Chart = new DonutChart() { Entries = entries };
            //chart1.Chart = new RadialGaugeChart() { Entries = entries };
            chart1.Chart = new RadarChart() { Entries = entries };


        }
    }
}
