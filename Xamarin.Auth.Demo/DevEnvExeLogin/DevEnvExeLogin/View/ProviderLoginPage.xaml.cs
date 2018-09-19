
using Xamarin.Forms;

namespace DevEnvExeLogin
{
    public partial class ProviderLoginPage : ContentPage
    {
        public string ProviderName { get; set; }
        public ProviderLoginPage(string _providername)
        {
            InitializeComponent();
            ProviderName = _providername;

        }
    }
}
