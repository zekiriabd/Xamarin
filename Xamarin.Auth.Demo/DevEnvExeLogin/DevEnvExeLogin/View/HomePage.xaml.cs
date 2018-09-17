using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DevEnvExeLogin
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            txUserID.Text = FacebookAuth.User.Id;
            txUserName.Text=  FacebookAuth.User.Name;
            //imgUserImage.Image= UIimage.LoadFormData
        }
    }
}
