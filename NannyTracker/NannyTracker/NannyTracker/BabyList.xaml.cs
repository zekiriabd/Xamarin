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
	public partial class BabyList : ContentPage
	{
		public BabyList ()
		{
			InitializeComponent ();
            this.BindingContext = new BabyListVm();
        }
	}
}