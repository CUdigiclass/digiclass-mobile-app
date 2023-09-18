using CULMS.Helpers;
using CULMS.Helpers.APIHelper;
using CULMS.Model.RequestModel;
using CULMS.View.Dashboard;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CULMS.ViewModel.DashboardVM
{
    public class QuizResultPageVM : ObservableObject
    {
        #region Private Properties

        #endregion

        #region Public Properties

        private double totalMarks;
        private double obtainMarks;
        private int attemptQuestion;
        private int totalQuestion;
        private int totalCorrectQs;
        private int totalInCorrectQs;

        public int TotalInCorrectQs
        {
            get { return totalInCorrectQs; }
            set { totalInCorrectQs = value; OnPropertyChanged(nameof(TotalInCorrectQs)); }
        }

        public int TotalCorrectQs
        {
            get { return totalCorrectQs; }
            set { totalCorrectQs = value; OnPropertyChanged(nameof(TotalCorrectQs)); }
        }

        public int TotalQuestion
        {
            get { return totalQuestion; }
            set { totalQuestion = value; OnPropertyChanged(nameof(TotalQuestion)); }
        }

        public int AttemptQuestion
        {
            get { return attemptQuestion; }
            set { attemptQuestion = value; OnPropertyChanged(nameof(AttemptQuestion)); }
        }

        public double ObtainMarks
        {
            get { return obtainMarks; }
            set { obtainMarks = value; OnPropertyChanged(nameof(ObtainMarks)); }
        }

        public double TotalMarks
        {
            get { return totalMarks; }
            set { totalMarks = value; OnPropertyChanged(nameof(TotalMarks)); }
        }

        #endregion

        #region Methods
        public QuizResultPageVM()
        {
            QuizResultMethod();
        }

        private async void QuizResultMethod()
        {
            try
            {
                IsLoading = true;
                StartQuizRequestModel startQuizRequest = new StartQuizRequestModel()
                {
                    //UserId = "O23MBA110345",
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    CourseQuizId = Utilities.courseQuizId
                    //CourseQuizId = 54
                };
                var response = await QuizResultAPI(startQuizRequest);
                var data = response.Data.FirstOrDefault();
                if (response != null && response.StatusCode == 200)
                {
                    TotalMarks = data.TotalMark;
                    ObtainMarks = data.ObtainedMarks;
                    AttemptQuestion = data.AttemptQuestion;
                    TotalQuestion = data.TotalQuestion;
                    TotalCorrectQs = data.TotalCorrectQs;
                    TotalInCorrectQs = data.TotalInCorrectQs;
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

        public Command HomeCommand => new Command((param) =>
        {
            try
            {
                IsLoading = true;
                Application.Current.MainPage = new HomePage();
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
        public async Task<QuizResultResponseModel> QuizResultAPI(StartQuizRequestModel startQuizRequest)
        {
            QuizResultResponseModel responseModel = new QuizResultResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(startQuizRequest);
                var quizResultURL = StringConstant.ApiKeyUrl + StringConstant.ApiKeyQuizResult;
                var apiResponse = await aPICall.PostRequestToken(quizResultURL, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        responseModel = JsonConvert.DeserializeObject<QuizResultResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }
        #endregion
    }
}
