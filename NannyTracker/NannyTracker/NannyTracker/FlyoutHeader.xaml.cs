using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NannyTracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FlyoutHeader : ContentView
	{
		public FlyoutHeader ()
		{
			InitializeComponent ();
            
            UserContent.BindingContext = new UserVm();

        }

        
	}
}