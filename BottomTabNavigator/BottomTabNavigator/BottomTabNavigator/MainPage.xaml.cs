using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace BottomTabNavigator
{
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetBarSelectedItemColor(Color.White);
            On<Android>().SetBarItemColor(Color.White);

            //On<iOS>().SetBarTintColor(Color.Red);                 // Unselected image+text color
            //On<iOS>().SetUnselectedItemTintColor(Color.White); // Selected image+text color
        }

        private void MyPage_CurrentPageChanged(object sender, System.EventArgs e)
        {
            MyPage.BarBackgroundColor = ((ContentPage)MyPage.CurrentPage).BackgroundColor;
        }
    }
}
