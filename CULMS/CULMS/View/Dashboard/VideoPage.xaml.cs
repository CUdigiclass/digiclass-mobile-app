using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPage : ContentPage
    {
        public VideoPage(string url)
        {
            try
            {
                InitializeComponent();
                busyindicator.IsRunning = true;
                busyindicator.IsVisible = true;
                mediaelement.Source = url;
                busyindicator.IsRunning = false;
                busyindicator.IsVisible = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}