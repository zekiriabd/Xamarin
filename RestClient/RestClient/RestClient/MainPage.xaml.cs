using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RestClient
{

    public class TUser
    {
        public int userId { get; set; }
        public string jobTitleName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }

    }
        public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGetInfo_Clicked(object sender, EventArgs e)
        {
             HttpClient oClient = new HttpClient();
           //  string strJson = await oClient.GetStringAsync("https://localhost:44390/api/values");

            string strJson = "{'userId':1,'jobTitleName':'Developer','firstName':'Aloui','lastName':'Ali','emailAddress':'zekiriabd@gmail.com'}";
            txJson.Text = strJson;
            TUser user = JsonConvert.DeserializeObject<TUser>(strJson);
            txId.Text = user.userId.ToString();
            txFirstName.Text = user.firstName;
            txLastName.Text = user.lastName;
            txJob.Text = user.jobTitleName;
            txEmail.Text = user.emailAddress;
            
        }
    }
}
