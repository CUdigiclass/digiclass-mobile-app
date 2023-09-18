using CULMS.Helpers;
using CULMS.Helpers.APIHelper;
using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using CULMS.Services;
using CULMS.View.Dashboard;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CULMS.ViewModel.DashboardVM
{
    public class AllCoursePageVM : ObservableObject
    {
        #region Private Properties

        private List<AllCourseData> allCourseList;
        #endregion

        #region Public Properties


        public List<AllCourseData> AllCourseList
        {
            get { return allCourseList; }
            set { allCourseList = value; OnPropertyChanged(nameof(AllCourseList)); }
        }

        #endregion

        #region Methods

        public AllCoursePageVM()
        {
            //GetCourseMethod();
            Data = new List<AllCourseData>();
        }

        private List<AllCourseData> data;

        public List<AllCourseData> Data
        {
            get { return data; }
            set { data = value; OnPropertyChanged(nameof(Data)); }
        }

        public async void GetCourseMethod(int pageno)
        {
            try
            {
                IsLoading = true;
                await Task.Delay(50);
                AllCoursesRequestModel allCoursesRequest = new AllCoursesRequestModel
                {
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    PageNo = pageno,
                    PageSize = 10,
                    RoleId = Preferences.Get(StringConstant.RoleId, 0)
                };
                var response = await GetCourselistWithoutEnrolledAPI(allCoursesRequest);
                if (response != null && response.StatusCode == 200)
                {                    
                    AllCourseList = new List<AllCourseData>(response.Data.Select(data => new AllCourseData()
                    {
                        AutherName = data.AutherName,
                        CourseName = data.CourseName,
                        BannerImageName = data.BannerImageName,
                        CourseId = data.CourseId,
                        CourseCode = data.CourseCode,
                        Semester = data.Semester,
                        Comments = data.Comments,
                        IsEnrolled = data.IsEnrolled,
                        Descriptions = data.Descriptions,
                        CategoryId = data.CategoryId,
                        Availble = data.Availble,
                        ContentViewConfigurationId = data.ContentViewConfigurationId,
                        CourseViewConfigurationId = data.CourseViewConfigurationId,
                        DEndDate = data.DEndDate,
                        DEndTime = data.DEndTime,
                        DisciplineId = data.DisciplineId,
                        DStartDate = data.DStartDate,
                        DStartTime = data.DStartTime,
                        DurationConfigurationId = data.DurationConfigurationId,
                        EnrolledCount = data.EnrolledCount,
                        FirstEnteredBy = data.FirstEnteredBy,
                        FirstEnteredOn = data.FirstEnteredOn,
                        GuestsPermitted = data.GuestsPermitted,
                        IsActive = data.IsActive,
                        IsDeleted = data.IsDeleted,
                        LastModifiedBy = data.LastModifiedBy,
                        LastModifiedOn = data.LastModifiedOn,
                        PageNo = data.PageNo,
                        PageSize = data.PageSize,
                        SubjectId = data.SubjectId,
                        UserId = data.UserId,
                        UserName = data.UserName
                    }));
                    var d = AllCourseList;
                    Data.AddRange(d);
                    AllCourseList = Data;
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
        }


        #endregion

        #region Commands


        public Command CourseDetailPageCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                Utilities.IsComeFromEnrollCourse = true;
                await Task.Delay(50);
                var data = param as AllCourseData;
                Utilities.courseId = data.CourseId;
               // await Application.Current.MainPage.Navigation.PushModalAsync(new CourseDetailPage(data));
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
        #endregion

        #region Api Methods

        public async Task<AllCoursesResponseModel> GetCourselistWithoutEnrolledAPI(AllCoursesRequestModel getEnrolledCourseRequest)
        {
            AllCoursesResponseModel allCoursesResponse = new AllCoursesResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(getEnrolledCourseRequest);
                var courseUrl = StringConstant.ApiKeyUrl + StringConstant.ApeKeyGetEnrollCourseListWithoutEnrollUser;
                var apiResponse = await aPICall.PostRequestToken(courseUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        allCoursesResponse = JsonConvert.DeserializeObject<AllCoursesResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return allCoursesResponse;
        }
        #endregion
    }
}
