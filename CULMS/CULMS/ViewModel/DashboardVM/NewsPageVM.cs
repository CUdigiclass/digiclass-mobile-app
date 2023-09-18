using CULMS.Helpers;
using CULMS.Helpers.APIHelper;
using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using CULMS.Services;
using CULMS.View.Dashboard;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CULMS.ViewModel.DashboardVM
{
    public class NewsPageVM : ObservableObject
    {
        #region Private Properties


        private ObservableCollection<NewsData> newsList;
        #endregion

        #region Public Properties
        private bool isVisibleWebView;
        private string sourceUrl;

        public string SourceUrl
        {
            get { return sourceUrl; }
            set { sourceUrl = value; OnPropertyChanged(nameof(SourceUrl)); }
        }

        public bool IsVisibleWebView
        {
            get { return isVisibleWebView; }
            set { isVisibleWebView = value; OnPropertyChanged(nameof(IsVisibleWebView)); }
        }

        public ObservableCollection<NewsData> NewsList
        {
            get { return newsList; }
            set { newsList = value; OnPropertyChanged(nameof(NewsList)); }
        }

        #endregion

        #region Methods

        public NewsPageVM()
        {
            IsVisibleWebView = false;
            GetNewsAPIMethod();
        }

        private async void GetNewsAPIMethod()
        {
            try
            {
                SemeterWithSubjectRequestModel semeterWithSubjectRequestModel = new SemeterWithSubjectRequestModel()
                {
                    UserId = ""
                };
                var response = await NewsAPI(semeterWithSubjectRequestModel);
                if (response != null && response.StatusCode == 200)
                {
                    NewsList = new ObservableCollection<NewsData>(response.Data.Select(data => new NewsData()
                    {
                        ThumbNailUrl = data.ThumbNailUrl,
                        NewsTitle = data.NewsTitle,
                        NewsDesc = data.NewsDesc,
                        SourceUrl = data.SourceUrl
                    }));
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Commands

        public Command ViewSourceCommand => new Command(async(param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as NewsData;
                if (data != null && data.SourceUrl != string.Empty)
                {
                    //Application.Current.MainPage.Navigation.PushModalAsync(new CommonWebPage(data.SourceUrl));
                    await RichNavigation.PushAsync(new CommonWebPage(data.SourceUrl), typeof(CommonWebPage));
                    //Application.Current.MainPage.Navigation.PushModalAsync(new CommonWebPage("https://www.cudigiclass.in/student-live-class-window/18344"));
                }
                else
                {
                   await Application.Current.MainPage.DisplayAlert("Alert", "No source Url.", "Ok");
                }
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

        #region API Methods

        public async Task<NewsResponseModel> NewsAPI(SemeterWithSubjectRequestModel semeterWithSubjectRequest)
        {
            NewsResponseModel newsResponseModel = new NewsResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(semeterWithSubjectRequest);
                var courseUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeyNews;
                var apiResponse = await aPICall.PostRequestToken(courseUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        newsResponseModel = JsonConvert.DeserializeObject<NewsResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return newsResponseModel;
        }
        #endregion
    }
}
