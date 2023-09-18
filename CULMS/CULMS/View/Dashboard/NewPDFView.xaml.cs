using Microsoft.AppCenter.Crashes;
using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPDFView : ContentPage
    {
        // string url = "http://media.wuerth.com/stmedia/shop/catalogpages/LANG_it/1637048.pdf";
        //string url = "https://culmsimages.s3.ap-south-1.amazonaws.com/course-1/Azure.txt";
        string URL1 = string.Empty;
        public NewPDFView(string url)
        {
            URL1 = url;
            InitializeComponent();
            if (Device.RuntimePlatform == Device.Android)
            {
                pdfView.Uri = url;
                pdfView.On<Android>().EnableZoomControls(true);
                pdfView.On<Android>().DisplayZoomControls(false);
            }
            else
            {
                pdfView.Source = url;
            }

        }

        [Obsolete]
        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                var URL = new Uri(URL1);
                //var d = new WebClient();
                //d.DownloadDataAsync(URL);
                Device.OpenUri(URL);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}