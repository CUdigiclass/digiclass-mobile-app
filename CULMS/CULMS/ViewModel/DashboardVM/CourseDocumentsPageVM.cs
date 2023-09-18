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
    public class CourseDocumentsPageVM : ObservableObject
    {
        #region Private Properties


        private ObservableCollection<CourseDocumentsData> courseDocumentList;
        #endregion

        #region Public Properties

        public ObservableCollection<CourseDocumentsData> CourseDocumentList
        {
            get { return courseDocumentList; }
            set { courseDocumentList = value; OnPropertyChanged(nameof(CourseDocumentList)); }
        }
        #endregion

        #region Methods

        public CourseDocumentsPageVM()
        {
            CourseDoucmentMethod();
        }
        public async void CourseDoucmentMethod()
        {
            try
            {
                CourseContentRequestModel courseContentRequest = new CourseContentRequestModel()
                {
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    RoleId = Preferences.Get(StringConstant.RoleId, 0),
                    CourseId = Utilities.courseId
                };
                var response = await CourseDocumentsAPI(courseContentRequest);
                if (response != null && response.StatusCode == 200)
                {
                    CourseDocumentList = new ObservableCollection<CourseDocumentsData>(response.Data.Select(data => new CourseDocumentsData()
                    {
                        Title = data.Title,
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

        public Command DocumentCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as CourseDocumentsData;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new NewPDFView(data.FileUrl));
                await RichNavigation.PushAsync(new NewPDFView(data.FileUrl), typeof(NewPDFView));
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

        public async Task<CourseDocumentsResponseModel> CourseDocumentsAPI(CourseContentRequestModel getEnrolledCourseRequest)
        {
            CourseDocumentsResponseModel moduleConfigurationResponse = new CourseDocumentsResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(getEnrolledCourseRequest);
                var moduleConfiguration = StringConstant.ApiKeyUrl + StringConstant.ApiKeyCourseDocuments;
                var apiResponse = await aPICall.PostRequestToken(moduleConfiguration, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        moduleConfigurationResponse = JsonConvert.DeserializeObject<CourseDocumentsResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return moduleConfigurationResponse;
        }
        #endregion

    }
}
