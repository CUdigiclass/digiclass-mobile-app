using CULMS.Helpers;
using CULMS.Helpers.APIHelper;
using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using CULMS.View.Dashboard;
using CULMS.Services;

namespace CULMS.ViewModel.DashboardVM
{
    public class EnrolledCoursePageVM : ObservableObject
    {
        #region Private Properties

        private ObservableCollection<EnrolledCourseDatum> enrolledCourseList;
        #endregion

        #region Public Properties



        public ObservableCollection<EnrolledCourseDatum> EnrolledCourseList
        {
            get { return enrolledCourseList; }
            set { enrolledCourseList = value; OnPropertyChanged(nameof(EnrolledCourseList)); }
        }

        #endregion

        #region Methods

        public EnrolledCoursePageVM()
        {
            //GetCourseMethod();
        }

        public async void GetCourseMethod()
        {
            try
            {
                IsLoading = true;
                await Task.Delay(50);
                GetEnrolledCourseRequestModel getEnrolledCourseRequest = new GetEnrolledCourseRequestModel
                {
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty)
                };
                var response = await GetEnrolledCourseAPI(getEnrolledCourseRequest);
                if (response != null && response.StatusCode == 200)
                {
                    EnrolledCourseList = new ObservableCollection<EnrolledCourseDatum>(response.data.Select(data => new EnrolledCourseDatum()
                    {
                        AutherName = data.AutherName,
                        CourseName = data.CourseName,
                        BannerImageName = data.BannerImageName,
                        CourseId = data.CourseId
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
        public Command CourseDetailPageCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                Utilities.IsComeFromEnrollCourse = false;
                await Task.Delay(50);
                var data = param as EnrolledCourseDatum;
                Utilities.courseId = data.CourseId;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new CourseDetailPage());
                await RichNavigation.PushAsync(new CourseDetailPage(), typeof(CourseDetailPage));
                //Utilities.IsComeFromEnrollCourse = true;
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
        public Command EnrollCourseFileListPageCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                await Task.Delay(50);
                var data = param as EnrolledCourseDatum;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new SemesterWithSubjectPage(data.CourseId));
                await RichNavigation.PushAsync(new SemesterWithSubjectPage(data.CourseId), typeof(SemesterWithSubjectPage));
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

        #region Api Methods

        public async Task<GetEnrolledCourseResponseModel> GetEnrolledCourseAPI(GetEnrolledCourseRequestModel getEnrolledCourseRequest)
        {
            GetEnrolledCourseResponseModel loginResponse = new GetEnrolledCourseResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(getEnrolledCourseRequest);
                var courseUrl = StringConstant.ApiKeyUrl + StringConstant.ApeKeyGetEnrolledCourse;
                var apiResponse = await aPICall.PostRequestToken(courseUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        loginResponse = JsonConvert.DeserializeObject<GetEnrolledCourseResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return loginResponse;
        }
        #endregion
    }
}
