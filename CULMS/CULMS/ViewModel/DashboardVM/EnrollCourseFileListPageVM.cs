using CULMS.Helpers;
using CULMS.Helpers.APIHelper;
using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using CULMS.View.Dashboard;
using CULMS.Services;

namespace CULMS.ViewModel.DashboardVM
{
    public class EnrollCourseFileListPageVM : ObservableObject
    {
        #region Private Properties

        private ObservableCollection<EnrollCourseFileDatum> enrollCourseFileList;
        #endregion

        #region Public Properties

        public ObservableCollection<EnrollCourseFileDatum> EnrollCourseFileList
        {
            get { return enrollCourseFileList; }
            set { enrollCourseFileList = value; OnPropertyChanged(nameof(EnrollCourseFileList)); }
        }

        #endregion

        #region Methods
        public async void GetEnrollCourseFileMethod(int courseId, string bannerImage)
        {
            try
            {
                IsLoading = true;
                await Task.Delay(50);
                EnrollCourseFileRequestModel getEnrolledCourseRequest = new EnrollCourseFileRequestModel
                {
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    CourseId = courseId
                };
                var response = await GetEnrollCourseFileListAPI(getEnrolledCourseRequest);
                if (response != null && response.StatusCode == 200)
                {
                    EnrollCourseFileList = new ObservableCollection<EnrollCourseFileDatum>(response.Data.Select(data => new EnrollCourseFileDatum()
                    {
                        CourseId = data.CourseId,
                        FileName = data.FileName,
                        FileUrl = data.FileUrl,
                        BannerImage = bannerImage,
                    }));

                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally { IsLoading = false; }
        }

        #endregion

        #region Commands

        public Command ContentCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                await Task.Delay(50);
                var data = param as EnrollCourseFileDatum;
                bool filterdata = data.FileUrl.Contains("mp4");
                if (filterdata)
                {
                    //await Application.Current.MainPage.Navigation.PushModalAsync(new VideoPage(data.FileUrl));
                    await RichNavigation.PushAsync(new VideoPage(data.FileUrl), typeof(VideoPage));
                }
                else
                {
                    //await Application.Current.MainPage.Navigation.PushModalAsync(new NewPDFView(data.FileUrl));
                    await RichNavigation.PushAsync(new NewPDFView(data.FileUrl), typeof(NewPDFView));
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally { IsLoading = false; }
        });
        #endregion

        #region API Methods

        public async Task<EnrollCourseFileResponseModel> GetEnrollCourseFileListAPI(EnrollCourseFileRequestModel enrollCourseFileRequest)
        {
            EnrollCourseFileResponseModel enrollCourseFileResponse = new EnrollCourseFileResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(enrollCourseFileRequest);
                var courseUrl = StringConstant.ApiKeyUrl + StringConstant.ApeKeyGetEnrollCourseFileList;
                var apiResponse = await aPICall.PostRequestToken(courseUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        enrollCourseFileResponse = JsonConvert.DeserializeObject<EnrollCourseFileResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return enrollCourseFileResponse;
        }
        #endregion
    }
}
