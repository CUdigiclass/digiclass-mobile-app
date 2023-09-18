using CULMS.Helpers;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;

namespace CULMS.ViewModel.DashboardVM
{
    public class VideoPageVM : ObservableObject
    {

        #region Private Properties

        private LayoutOptions videoVerticalOption;
        private string zoomImage;
       // private FillMode videoFillMode;
        #endregion

        #region Public Properties

        public string ZoomImage
        {
            get { return zoomImage; }
            set { zoomImage = value; OnPropertyChanged(nameof(ZoomImage)); }
        }

        public LayoutOptions VideoVerticalOption
        {
            get { return videoVerticalOption; }
            set { videoVerticalOption = value; OnPropertyChanged(nameof(VideoVerticalOption)); }
        }
        //public FillMode VideoFillMode
        //{
        //    get { return videoFillMode; }
        //    set { videoFillMode = value; OnPropertyChanged(nameof(VideoFillMode)); }
        //}

        #endregion

        #region Methods

        public VideoPageVM()
        {
            ZoomImage = "zoom_in";
            VideoVerticalOption = LayoutOptions.CenterAndExpand;
        }
        #endregion

        #region Commands
        public Command ZoomCommand => new Command(() =>
        {
            if (ZoomImage == "zoom_in")
            {
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    MessagingCenter.Send(this, "SetLandscapeModeOn");
                    VideoVerticalOption = LayoutOptions.FillAndExpand;
                    //VideoFillMode = FillMode.Resize;
                }
                ZoomImage = "zoom_out";
            }
            else
            {
                MessagingCenter.Send(this, "SetLandscapeModeOff");
                VideoVerticalOption = LayoutOptions.CenterAndExpand;
                ZoomImage = "zoom_in";
            }
        });

        public Command VideoBackCommand => new Command(() =>
        {
            try
            {
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    MessagingCenter.Send(this, "SetLandscapeModeOff");
                    VideoVerticalOption = LayoutOptions.CenterAndExpand;
                }
                App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });

        #endregion
    }
}
