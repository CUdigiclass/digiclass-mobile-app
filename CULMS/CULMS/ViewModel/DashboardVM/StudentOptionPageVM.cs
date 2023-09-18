using CULMS.Helpers;
using CULMS.Services;
using CULMS.View.Dashboard;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;

namespace CULMS.ViewModel.DashboardVM
{
    public class StudentOptionPageVM : ObservableObject
    {
        #region Private Properties


        #endregion

        #region Public Properties

        private int configurationId;

        public int ConfigurationId
        {
            get { return configurationId; }
            set { configurationId = value; OnPropertyChanged(nameof(ConfigurationId)); }
        }

        #endregion

        #region Methods

        #endregion

        #region Command

        public Command DiscussionForumCommadn => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as StudentOptionPageVM;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new DiscussionFormPage(data.ConfigurationId));
                await RichNavigation.PushAsync(new DiscussionFormPage(data.ConfigurationId), typeof(DiscussionFormPage));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        });
        public Command LiveRecordingClassCommadn => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as StudentOptionPageVM;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new LiveClassRecodingPage());
                await RichNavigation.PushAsync(new LiveClassRecodingPage(),typeof(LiveClassRecodingPage));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        });
        #endregion
    }
}
