using CULMS.Services;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;

namespace CULMS.Helpers
{
    public class ObservableObject : BindableObject
    {
        #region private Properties

        private bool isLoading;
        #endregion

        #region Public Properties

        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        #endregion

        #region Commands

        #endregion

        #region Methods
        public Command BackCommand => new Command(async () =>
        {
            try
            {
                //App.Current.MainPage.Navigation.PopModalAsync();
                await RichNavigation.PopAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });
        public Command BackPopModelCommand => new Command(async () =>
        {
            try
            {
                //App.Current.MainPage.Navigation.PopModalAsync();
                await RichNavigation.PopModelAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });

        #endregion
    }
}
