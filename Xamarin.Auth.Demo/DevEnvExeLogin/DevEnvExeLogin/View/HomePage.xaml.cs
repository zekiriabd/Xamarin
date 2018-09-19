using FacebookLogin.Models;
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
            txUserID.Text = ((GUserM)FacebookAuth.User).id;
            txUserName.Text= ((GUserM)FacebookAuth.User).name;
            imgUserProfile.Source = ((GUserM)FacebookAuth.User).picture;
            //imgUserImage.Image= UIimage.LoadFormData
        }
    }
}
