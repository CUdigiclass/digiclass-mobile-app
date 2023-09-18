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

    public class QuizListPageVM : ObservableObject
    {
        #region Private Properties

      
        private ObservableCollection<UnGradedAssessment> ungradedQuizList;
        private bool isVisbleUngradedQuiz;
        private bool isVisbleGradedQuiz;
        private ObservableCollection<GradedAssessment> gradedQuizList;
        #endregion

        #region Public Properties
        private bool isvisibleQuizTypeStack;

        public bool IsvisibleQuizTypeStack
        {
            get { return isvisibleQuizTypeStack; }
            set { isvisibleQuizTypeStack = value; OnPropertyChanged(nameof(IsvisibleQuizTypeStack)); }
        }

        public ObservableCollection<GradedAssessment> GradedQuizList
        {
            get { return gradedQuizList; }
            set { gradedQuizList = value; OnPropertyChanged(nameof(GradedQuizList)); }
        }

        public bool IsVisbleGradedQuiz
        {
            get { return isVisbleGradedQuiz; }
            set { isVisbleGradedQuiz = value; OnPropertyChanged(nameof(IsVisbleGradedQuiz)); }
        }

        public bool IsVisbleUngradedQuiz
        {
            get { return isVisbleUngradedQuiz; }
            set { isVisbleUngradedQuiz = value; OnPropertyChanged(nameof(IsVisbleUngradedQuiz)); }
        }

        public ObservableCollection<UnGradedAssessment> UngradedQuizList
        {
            get { return ungradedQuizList; }
            set { ungradedQuizList = value; OnPropertyChanged(nameof(UngradedQuizList)); }
        }

        #endregion

        #region Methods

        public QuizListPageVM()
        {           
            IsvisibleQuizTypeStack = true;
            IsVisbleGradedQuiz = false;
            IsVisbleUngradedQuiz = false;
            QuizListMethod();
        }

        public async void QuizListMethod()
        {
            try
            {
                CourseContentRequestModel courseContentRequest = new CourseContentRequestModel()
                {
                    CourseId = Utilities.courseId,
                    RoleId = Preferences.Get(StringConstant.RoleId, 0)
                };
                var response = await GetQuizListAPI(courseContentRequest);
                if (response != null && response.StatusCode == 200)
                {
                    UngradedQuizList = new ObservableCollection<UnGradedAssessment>(response.Data.UnGradedAssessment.Select(x => new UnGradedAssessment()
                    {
                        QuizName = x.QuizName,
                        TopicName = x.TopicName,
                        TimeDuration = x.TimeDuration,
                        Attempt = x.Attempt,
                        Id = x.Id,
                    }));
                    GradedQuizList = new ObservableCollection<GradedAssessment>(response.Data.GradedAssessment.Select(x => new GradedAssessment()
                    {
                        QuizName = x.QuizName,
                        TopicName = x.TopicName,
                        TimeDuration = x.TimeDuration,
                        Attempt = x.Attempt,
                        Id = x.Id,
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

        public Command UngradedQuizListCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as UnGradedAssessment;
                Utilities.savedQuizTime = data.TimeDuration;
                if (data != null)
                {
                    StartQuizRequestModel startQuizRequest = new StartQuizRequestModel()
                    {
                        UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                        CourseQuizId = data.Id
                    };
                    var response = await StartQuizAPI(startQuizRequest);
                    if (response != null && response.StatusCode == 200 && response.Data ==true)
                    {
                        //await Application.Current.MainPage.Navigation.PushModalAsync(new QuizPage(data.Id,data.TimeDuration));
                        await RichNavigation.PushAsync(new QuizPage(data.Id,data.TimeDuration),typeof(QuizPage));
                    }
                    else if(response != null && response.StatusCode == 200 && response.Data == false)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert!", "Quiz Already Submitted.", "Ok");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert!", response.Message, "Ok");
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
        public Command GradedQuizListCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as GradedAssessment;
                Utilities.savedQuizTime = data.TimeDuration;
                if (data != null)
                {
                    StartQuizRequestModel startQuizRequest = new StartQuizRequestModel()
                    {
                        UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                        CourseQuizId = data.Id
                    };
                    var response = await StartQuizAPI(startQuizRequest);
                    if (response != null && response.StatusCode == 200 && response.Data == true)
                    {
                        //await Application.Current.MainPage.Navigation.PushModalAsync(new QuizPage(data.Id));
                        await RichNavigation.PushAsync(new QuizPage(data.Id), typeof(QuizPage));
                    }
                    else if (response != null && response.StatusCode == 200 && response.Data == false)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Quiz Already Submitted.", "Ok");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
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

        public Command UngradedQuizCommand => new Command((param) =>
        {
            IsvisibleQuizTypeStack = false;
            IsVisbleUngradedQuiz = true;
            IsVisbleGradedQuiz = false;
        });

        public Command GradedQuizCommand => new Command((param) =>
        {
            IsvisibleQuizTypeStack = false;
            IsVisbleUngradedQuiz = false;
            IsVisbleGradedQuiz = true;
        });
        #endregion

        #region API Methods

        public async Task<QuizListResponseModel> GetQuizListAPI(CourseContentRequestModel courseContentRequest)
        {
            QuizListResponseModel allCoursesResponse = new QuizListResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(courseContentRequest);
                var courseUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeyGetQuizList;
                var apiResponse = await aPICall.PostRequestToken(courseUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        allCoursesResponse = JsonConvert.DeserializeObject<QuizListResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return allCoursesResponse;
        }
        public async Task<StartQuizResponseModel> StartQuizAPI(StartQuizRequestModel startQuizRequest)
        {
            StartQuizResponseModel startQuizResponse = new StartQuizResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(startQuizRequest);
                var startQuizUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeyStartQuiz;
                var apiResponse = await aPICall.PostRequestToken(startQuizUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        startQuizResponse = JsonConvert.DeserializeObject<StartQuizResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return startQuizResponse;
        }
        #endregion

      
    }
}
