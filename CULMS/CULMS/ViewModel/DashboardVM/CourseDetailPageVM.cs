using CULMS.Helpers;
using CULMS.Helpers.APIHelper;
using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using CULMS.Services;
using CULMS.View.Dashboard;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CULMS.ViewModel.DashboardVM
{
    public class CourseDetailPageVM : ObservableObject
    {

        #region Private Properties

        private string courseImage;
        private string courseName;
        private string courseCode;
        private int enrolledCount;
        private int semester;
        private string description;
        private string firstEntryBy;
        private DateTime startDate;
        private DateTime endDate;
        private int courseId;
        private string userId;
        private bool isEnableEnrollBtn;
        private string courseStatus;

        #endregion

        #region Public Properties
        private bool isVisibleEnrollBtn;
        private string courseOutCome;
        private bool isVisibleViewCourseBtn;
        private int courseCredit;

        public int CourseCredit
        {
            get { return courseCredit; }
            set { courseCredit = value; OnPropertyChanged(nameof(CourseCredit)); }
        }

        public bool IsVisibleViewCourseBtn
        {
            get { return isVisibleViewCourseBtn; }
            set { isVisibleViewCourseBtn = value; OnPropertyChanged(nameof(IsVisibleViewCourseBtn)); }
        }

        public string CourseOutCome
        {
            get { return courseOutCome; }
            set { courseOutCome = value; OnPropertyChanged(nameof(CourseOutCome)); }
        }

        public bool IsVisibleEnrollBtn
        {
            get { return isVisibleEnrollBtn; }
            set { isVisibleEnrollBtn = value; OnPropertyChanged(nameof(IsVisibleEnrollBtn)); }
        }
        public string CourseStatus
        {
            get { return courseStatus; }
            set { courseStatus = value; OnPropertyChanged(nameof(CourseStatus)); }
        }

        public bool IsEnableEnrollBtn
        {
            get { return isEnableEnrollBtn; }
            set { isEnableEnrollBtn = value; OnPropertyChanged(nameof(IsEnableEnrollBtn)); }
        }

        public string UserId
        {
            get { return userId; }
            set { userId = value; OnPropertyChanged(nameof(UserId)); }
        }
        public int CourseId
        {
            get { return courseId; }
            set { courseId = value; OnPropertyChanged(nameof(CourseId)); }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; OnPropertyChanged(nameof(EndDate)); }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }
        public string FirstEntryBy
        {
            get { return firstEntryBy; }
            set { firstEntryBy = value; OnPropertyChanged(nameof(FirstEntryBy)); }
        }
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }
        public int Semester
        {
            get { return semester; }
            set { semester = value; OnPropertyChanged(nameof(Semester)); }
        }
        public int EnrolledCount
        {
            get { return enrolledCount; }
            set { enrolledCount = value; OnPropertyChanged(nameof(EnrolledCount)); }
        }
        public string CourseCode
        {
            get { return courseCode; }
            set { courseCode = value; OnPropertyChanged(nameof(CourseCode)); }
        }
        public string CourseImage
        {
            get { return courseImage; }
            set { courseImage = value; OnPropertyChanged(nameof(CourseImage)); }
        }
        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; OnPropertyChanged(nameof(CourseName)); }
        }

        #endregion

        #region Methods

        public CourseDetailPageVM()
        {
            if (Utilities.IsComeFromEnrollCourse)
            {
                IsVisibleEnrollBtn = true;
                IsVisibleViewCourseBtn = false;
            }
            else
            {
                IsVisibleEnrollBtn = false;
                IsVisibleViewCourseBtn = true;
            }
            CourseDetailMethod();
        }

        private async void CourseDetailMethod()
        {
            try
            {
                IsLoading = true;
                var data = await CourseDetailAPI();
                if (data != null)
                {
                    CourseImage = data.Data.BannerImageName;
                    CourseName = data.Data.CourseName;
                    CourseCode = data.Data.CourseCode;
                    EnrolledCount = data.Data.EnrolledCount;
                    Semester = data.Data.Semester;
                    CourseStatus = data.Data.CourseStatus;
                    StartDate = data.Data.DStartDate;
                    EndDate = data.Data.DEndDate;
                    Description = data.Data.Descriptions;
                    CourseOutCome = data.Data.CourseOutCome;
                    CourseId = data.Data.CourseId;
                    CourseCredit = data.Data.CourseCredit;
                    //IsEnableEnrollBtn = data.Data.IsEnrolled ? false : true;
                    IsVisibleEnrollBtn = data.Data.IsEnrolled ? false : true;
                    IsVisibleViewCourseBtn = data.Data.IsEnrolled ? true : false;
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

        //public void CourseDetailMethod(AllCourseData data)
        //{
        //    try
        //    {
        //        if (data != null)
        //        {
        //            CourseImage = data.BannerImageName;
        //            CourseName = data.CourseName;
        //            CourseCode = data.CourseCode;
        //            EnrolledCount = data.EnrolledCount;
        //            Semester = data.Semester;
        //            FirstEntryBy = data.FirstEnteredBy;
        //            StartDate = data.DStartDate;
        //            EndDate = data.DEndDate;
        //            Description = data.Descriptions;
        //            CourseId = data.CourseId;
        //            UserId = data.UserId;
        //            IsEnableEnrollBtn = data.IsEnrolled ? false : true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Crashes.TrackError(ex);
        //    }
        //}
        #endregion

        #region Commands

        public Command DiscussionCommand => new Command(async (param) =>
        {
            var data = param as CourseDetailPageVM;
            if (data != null)
            {
                //await Application.Current.MainPage.Navigation.PushModalAsync(new DiscussionFormPage(data.courseId));
            }
        });
        public Command ViewCourseCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as CourseDetailPageVM;
                Utilities.courseId = data.CourseId;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new CourseModulePage());
                await RichNavigation.PushAsync(new CourseModulePage(), typeof(CourseModulePage));
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
        public Command EnrollCourseCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as CourseDetailPageVM;
                if (data != null)
                {
                    EnrollCourseRequestModel enrollCourseRequest = new EnrollCourseRequestModel
                    {
                        CourseId = data.CourseId,
                        LearnerUserId = Preferences.Get(StringConstant.UserId, string.Empty)
                    };
                    var response = await GetEnrolledCourseAPI(enrollCourseRequest);
                    if (response != null && response.StatusCode == 200)
                    {
                        IsVisibleEnrollBtn = false;
                        await Application.Current.MainPage.DisplayAlert(" ", "Course Enrolled Successfully.", "Ok");
                        //IsLoading = true;
                        //await Task.Delay(100);
                        //IsLoading = false;
                        IsVisibleViewCourseBtn = true;
                        //await Application.Current.MainPage.DisplayAlert("Alert", "Course Enroll Successfully.", "Ok");
                        //Application.Current.MainPage = new HomePage();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
                        Application.Current.MainPage =new NavigationPage(new HomePage());
                    }
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

        #region API Call Methods
        public async Task<CourseDetailResponseModel> CourseDetailAPI()
        {
            CourseDetailResponseModel courseDetailResponse = new CourseDetailResponseModel();
            try
            {
                //Student/GetByCourseDetailsId?courseId=84&userId=student123%40cumail.in
                APICall aPICall = new APICall();
                var loginUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeyCourseDetail + Utilities.courseId + "&userId=" + Preferences.Get(StringConstant.UserId, string.Empty);
                var apiResponse = await aPICall.GetRequest(loginUrl, Preferences.Get("Token", string.Empty)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        courseDetailResponse = JsonConvert.DeserializeObject<CourseDetailResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return courseDetailResponse;
        }
        public async Task<EnrollCourseResponseModel> GetEnrolledCourseAPI(EnrollCourseRequestModel getEnrolledCourseRequest)
        {
            EnrollCourseResponseModel enrollCourseResponse = new EnrollCourseResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(getEnrolledCourseRequest);
                var courseUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeyEnrollCourse;
                var apiResponse = await aPICall.PostRequestToken(courseUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        enrollCourseResponse = JsonConvert.DeserializeObject<EnrollCourseResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return enrollCourseResponse;
        }
        #endregion
    }
}
