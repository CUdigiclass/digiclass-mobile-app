using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.PartialView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TabbedView : ContentView
	{
		public TabbedView ()
		{
			InitializeComponent ();
		}
	}
}