using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LiveClassRecodingPage : ContentPage
	{
        int pagenumber = 1;
        public LiveClassRecodingPage ()
		{
			InitializeComponent ();
            vm.RecordingLiveClass(pagenumber);
        }
        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            var scrollView = sender as ScrollView;
            double scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;
            if (scrollingSpace <= e.ScrollY)
            {
                //reached end
                pagenumber++;
                vm.RecordingLiveClass(pagenumber);
            }
        }
    }
}