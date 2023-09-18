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
    public class CourseContentPageVM : ObservableObject
    {
        #region Private Properties

        #endregion

        #region Public Properties

        private ObservableCollection<CourseContentData> contentVideoList;
        private bool isVisibleVideoList;
        private bool isVisiblePDfList;
        private ObservableCollection<CourseContentData> contentPDFList;

        public bool IsVisiblePDfList
        {
            get { return isVisiblePDfList; }
            set { isVisiblePDfList = value; OnPropertyChanged(nameof(IsVisiblePDfList)); }
        }

        public bool IsVisibleVideoList
        {
            get { return isVisibleVideoList; }
            set { isVisibleVideoList = value; OnPropertyChanged(nameof(IsVisibleVideoList)); }
        }

        public ObservableCollection<CourseContentData> ContentPDFList
        {
            get { return contentPDFList; }
            set { contentPDFList = value;OnPropertyChanged(nameof(ContentPDFList)); }
        }

        public ObservableCollection<CourseContentData> ContentVideoList
        {
            get { return contentVideoList; }
            set { contentVideoList = value; OnPropertyChanged(nameof(ContentVideoList)); }
        }

        #endregion

        #region Methods

        public CourseContentPageVM()
        {
            if (Utilities.ContentType == 26)
            {
                CourseContentMethod();
                IsVisiblePDfList = false;
                IsVisibleVideoList = true;
            }
            else if (Utilities.ContentType == 27)
            {
                CourseContentPDFMethod();
                IsVisibleVideoList = false;
                IsVisiblePDfList = true;
            }
           
        }

        private async void CourseContentPDFMethod()
        {
            try
            {
                CourseContentRequestModel courseContentRequest = new CourseContentRequestModel()
                {
                    SubjectId = 0,
                    WeekId = 0,
                    IsWeek = false,
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    RoleId = Preferences.Get(StringConstant.RoleId, 0),
                    CourseId = Utilities.courseId,
                    ContentType = Utilities.ContentType,
                    CourseModuleId = Utilities.courseModuleId
                };
                var response = await CourseContentAPI(courseContentRequest);
                if (response != null && response.StatusCode == 200)
                {
                    ContentPDFList = new ObservableCollection<CourseContentData>(response.Data.Select(data => new CourseContentData()
                    {
                        TopicName = data.TopicName,
                        FileUrl = data.FileUrl,
                    }));
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        private async void CourseContentMethod()
        {
            try
            {
                CourseContentRequestModel courseContentRequest = new CourseContentRequestModel()
                {
                    SubjectId = 0,
                    WeekId = 0,
                    IsWeek = false,
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    RoleId = Preferences.Get(StringConstant.RoleId, 0),
                    CourseId = Utilities.courseId,
                    ContentType = Utilities.ContentType,
                    CourseModuleId = Utilities.courseModuleId
                };
                var response = await CourseContentAPI(courseContentRequest);
                if (response != null && response.StatusCode == 200)
                {
                    ContentVideoList = new ObservableCollection<CourseContentData>(response.Data.Select(data => new CourseContentData()
                    {
                        TopicName = data.TopicName,
                        FileUrl = data.FileUrl,
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

        public Command VideoCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as CourseContentData;
                if (data != null)
                {
                   // await Application.Current.MainPage.Navigation.PushModalAsync(new VideoPage(data.FileUrl));
                    await RichNavigation.PushAsync(new VideoPage(data.FileUrl),typeof(VideoPage));

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
        public Command PDFCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as CourseContentData;
                if (data != null)
                {
                    //await Application.Current.MainPage.Navigation.PushModalAsync(new NewPDFView(data.FileUrl));
                    await RichNavigation.PushAsync(new NewPDFView(data.FileUrl), typeof(NewPDFView));
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

        public async Task<CourseContentResponseModel> CourseContentAPI(CourseContentRequestModel coursecontentrequestModel)
        {
            CourseContentResponseModel courseContentResponse = new CourseContentResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(coursecontentrequestModel);
                var coursecontentUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeyCourseContent;
                var apiResponse = await aPICall.PostRequestToken(coursecontentUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        courseContentResponse = JsonConvert.DeserializeObject<CourseContentResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return courseContentResponse;
        }
        #endregion
    }
}
