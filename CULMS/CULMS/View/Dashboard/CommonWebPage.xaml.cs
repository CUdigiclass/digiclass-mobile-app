using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CommonWebPage : ContentPage
	{
		public CommonWebPage (string URL)
		{
			InitializeComponent ();
            webView.Source = URL;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await progress.ProgressTo(0.9, 900, Easing.SpringIn);
        }

        protected void OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            progress.IsVisible = true;
        }

        protected void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            progress.IsVisible = false;
        }
        //async void OnBackButtonClicked(object sender, EventArgs e)
        //{
        //    if (webView.CanGoBack)
        //    {
        //        webView.GoBack();
        //    }
        //    else
        //    {
        //        await Navigation.PopAsync();
        //    }
        //}

        //void OnForwardButtonClicked(object sender, EventArgs e)
        //{
        //    if (webView.CanGoForward)
        //    {
        //        webView.GoForward();
        //    }
        //}
    }
}