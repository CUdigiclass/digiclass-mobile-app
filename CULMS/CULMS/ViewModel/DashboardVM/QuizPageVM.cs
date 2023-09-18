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
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CULMS.ViewModel.DashboardVM
{
    public class QuizPageVM : ObservableObject
    {
        #region Private Properties

        private Timer _timer;
        private ObservableCollection<QuestionData> answerList;
        private int QuestionIndex = 0;
        #endregion

        #region Public Properties 
        private string buttonText;
        private bool isVisiblePreviosBtn;
        private bool isVisbleSuccessFrame;
        private bool isVisbleTimeOverFrame;
        private bool isVisibleBackFrame;
        private bool isVisibleNextBtn;

        public bool IsVisibleNextBtn
        {
            get { return isVisibleNextBtn; }
            set { isVisibleNextBtn = value; OnPropertyChanged(nameof(IsVisibleNextBtn)); }
        }

        public bool IsVisibleBackFrame
        {
            get { return isVisibleBackFrame; }
            set { isVisibleBackFrame = value; OnPropertyChanged(nameof(IsVisibleBackFrame)); }
        }

        public bool IsVisbleTimeOverFrame
        {
            get { return isVisbleTimeOverFrame; }
            set { isVisbleTimeOverFrame = value; OnPropertyChanged(nameof(IsVisbleTimeOverFrame)); }
        }

        public bool IsVisbleSuccessFrame
        {
            get { return isVisbleSuccessFrame; }
            set { isVisbleSuccessFrame = value; OnPropertyChanged(nameof(IsVisbleSuccessFrame)); }
        }

        public bool IsVisiblePreviosBtn
        {
            get { return isVisiblePreviosBtn; }
            set { isVisiblePreviosBtn = value; OnPropertyChanged(nameof(IsVisiblePreviosBtn)); }
        }

        public string ButtonText
        {
            get { return buttonText; }
            set { buttonText = value; OnPropertyChanged(nameof(ButtonText)); }
        }

        private Color answerBackgroundColor;
        private Color answerTextColor;
        private ObservableCollection<QuestionData> tempAnswerList;

        public ObservableCollection<QuestionData> TempAnswerList
        {
            get { return tempAnswerList; }
            set { tempAnswerList = value; OnPropertyChanged(nameof(TempAnswerList)); }
        }

        public ObservableCollection<QuestionData> AnswerList
        {
            get { return answerList; }
            set { answerList = value; OnPropertyChanged(nameof(AnswerList)); }
        }
        public Color AnswerTextColor
        {
            get { return answerTextColor; }
            set { answerTextColor = value; OnPropertyChanged(nameof(AnswerTextColor)); }
        }

        public Color AnswerBackgroundColor
        {
            get { return answerBackgroundColor; }
            set { answerBackgroundColor = value; OnPropertyChanged(nameof(AnswerBackgroundColor)); }
        }
        private ObservableCollection<testModel> testList;

        public ObservableCollection<testModel> TestList
        {
            get { return testList; }
            set { testList = value; OnPropertyChanged(nameof(TestList)); }
        }

        #endregion

        #region Methods

        public QuizPageVM()
        {
            //make sure you put this in the constructor
            _timer = new Timer(TimeSpan.FromSeconds(1), CountDown);
            TotalSeconds = TimeSpan.FromMinutes(Utilities.savedQuizTime);
            IsVisibleNextBtn = true;
            ButtonText = "Next";
            IsVisibleBackFrame = false;
            TempAnswerList = new ObservableCollection<QuestionData>();
        }
        int courseQuizId = 0;
        double quizTime;
        public async void QuizAPIMethod(int id, int timeduration)
        {
            try
            {
                IsLoading = true;
                var response = await QuizAPI(id);
                quizTime = timeduration;
                if (response != null && response.StatusCode == 200)
                {
                    courseQuizId = response.Data.Id;
                    Utilities.courseQuizId = courseQuizId;
                    ObservableCollection<testModel> abc = new ObservableCollection<testModel>();
                    AnswerList = new ObservableCollection<QuestionData>();
                    ObservableCollection<testModel> AnswerNameList = new ObservableCollection<testModel>();
                    int i = 1;
                    foreach (var item in response.Data.Questions)
                    {
                        AnswerNameList = new ObservableCollection<testModel>();
                        AnswerNameList.Add(new testModel() { AnswerName = item.Option1, OptionName = "A", Id = item.Id });
                        AnswerNameList.Add(new testModel() { AnswerName = item.Option2, OptionName = "B", Id = item.Id });
                        AnswerNameList.Add(new testModel() { AnswerName = item.Option3, OptionName = "C", Id = item.Id });
                        AnswerNameList.Add(new testModel() { AnswerName = item.Option4, OptionName = "D", Id = item.Id });
                        TempAnswerList.Add(new QuestionData() { Id = item.Id, QuestionName = item.QuestionName, QuestionNumber = i++, TestList = AnswerNameList });
                    }
                    AnswerList.Add(TempAnswerList[QuestionIndex]);
                    _timer.Start();
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
        string selectedOption = string.Empty;
        int quizQustionId = 0;
        public Command ShowBackFrameCommand => new Command(() =>
        {
            try
            {
                IsVisibleBackFrame = true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });
        public Command HideBackPopupCommand => new Command(() =>
        {
            try
            {
                IsVisibleBackFrame = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });
        public Command BackCmd => new Command(async () =>
        {
            try
            {
                _timer.Stop();
                IsVisibleBackFrame = false;
               // await Application.Current.MainPage.Navigation.PopAsync();
               await RichNavigation.PopAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });
        public Command AnswerSelectedCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as testModel;
                var items = AnswerList;
                selectedOption = data.OptionName;
                quizQustionId = data.Id;
                foreach (var item in items[0].TestList)
                {
                    item.AnswerBackgroundColor = Color.WhiteSmoke;
                    item.AnswerTextColor = Color.Black;
                }
                data.AnswerBackgroundColor = Color.Gray;
                data.AnswerTextColor = Color.White;
                AddQuizQuestionAttemptRequestModel attemptRequestModel = new AddQuizQuestionAttemptRequestModel()
                {
                    StudentAnswer = selectedOption,
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    CourseQuizId = courseQuizId,
                    QuizQuestionId = quizQustionId
                };
                var response = await AddQuizQuestionAttemptAPI(attemptRequestModel);
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

        public Command NextCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as QuizPageVM;
                QuestionIndex++;
                AnswerList = new ObservableCollection<QuestionData>();
                int totalcount = TempAnswerList.Count - 1;
                if (data.ButtonText == "Submit")
                {
                    //AddQuizQuestionAttemptRequestModel attemptRequestModel = new AddQuizQuestionAttemptRequestModel()
                    //{
                    //    StudentAnswer = selectedOption,
                    //    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    //    CourseQuizId = courseQuizId,
                    //    QuizQuestionId = quizQustionId
                    //};
                    //var response = await AddQuizQuestionAttemptAPI(attemptRequestModel);
                    StartQuizRequestModel startQuizRequest = new StartQuizRequestModel()
                    {
                        UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                        CourseQuizId = courseQuizId
                    };
                    var finalSumbitQuizResponse = await FinalSubmitAPI(startQuizRequest);
                    if (finalSumbitQuizResponse != null && finalSumbitQuizResponse.StatusCode == 200)
                    {
                        IsVisiblePreviosBtn = false;
                        IsVisibleNextBtn = false;
                        IsVisbleSuccessFrame = true;
                        _timer.Stop();
                        await Task.Delay(1000);
                        IsVisbleSuccessFrame = false;
                        Application.Current.MainPage = new QuizResultPage();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert!", finalSumbitQuizResponse.Message, "Ok");
                    }
                }
                if (TempAnswerList.Count > QuestionIndex)
                {
                    //AddQuizQuestionAttemptRequestModel attemptRequestModel = new AddQuizQuestionAttemptRequestModel()
                    //{
                    //    StudentAnswer = selectedOption,
                    //    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    //    CourseQuizId = courseQuizId,
                    //    QuizQuestionId = quizQustionId
                    //};
                    //var response = await AddQuizQuestionAttemptAPI(attemptRequestModel);
                    //if (response != null && response.StatusCode == 200)
                    //{
                    AnswerList.Add(TempAnswerList[QuestionIndex]);
                    IsVisiblePreviosBtn = true;
                    if (totalcount == QuestionIndex)
                    {
                        ButtonText = "Submit";
                    }
                    //}
                    //else
                    //{
                    //    await Application.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
                    //}
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
        public Command PreviousCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as QuizPageVM;
                QuestionIndex--;
                AnswerList = new ObservableCollection<QuestionData>();
                if (TempAnswerList.Count > QuestionIndex && QuestionIndex >= 0)
                {
                    AnswerList.Add(TempAnswerList[QuestionIndex]);
                    ButtonText = "Next";
                }
                if (QuestionIndex == 0)
                {
                    IsVisiblePreviosBtn = false;
                    ButtonText = "Next";
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

        #region Api Methods
        public async Task<QuizResponseModel> QuizAPI(int id)
        {
            QuizResponseModel quizResponse = new QuizResponseModel();
            try
            {
                APICall aPICall = new APICall();
                //string jsonRequest = JsonConvert.SerializeObject(quizRequest);
                var courseUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeyQuiz + id;
                var apiResponse = await aPICall.GetRequest(courseUrl, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        quizResponse = JsonConvert.DeserializeObject<QuizResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return quizResponse;
        }
        public async Task<StartQuizResponseModel> AddQuizQuestionAttemptAPI(AddQuizQuestionAttemptRequestModel addQuizQuestionAttemptRequest)
        {
            StartQuizResponseModel responseModel = new StartQuizResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(addQuizQuestionAttemptRequest);
                var addattemptUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeyAddQuizQuestionAttempt;
                var apiResponse = await aPICall.PostRequestToken(addattemptUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        responseModel = JsonConvert.DeserializeObject<StartQuizResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }
        public async Task<StartQuizResponseModel> FinalSubmitAPI(StartQuizRequestModel startQuizRequest)
        {
            StartQuizResponseModel responseModel = new StartQuizResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(startQuizRequest);
                var finalSubmitQuizUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeyFinalSubmitQuiz;
                var apiResponse = await aPICall.PostRequestToken(finalSubmitQuizUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        responseModel = JsonConvert.DeserializeObject<StartQuizResponseModel>(apiReponseData);
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

        #region Timer

        // private TimeSpan _totalSeconds = new TimeSpan(0, 0, 0, 10);
        private TimeSpan _totalSeconds;

        public TimeSpan TotalSeconds
        {
            get { return _totalSeconds; }
            set { _totalSeconds = value; OnPropertyChanged(nameof(TotalSeconds)); }
        }

        /// <summary>
        /// Counts down the timer
        /// </summary>
        private async void CountDown()
        {
            if (_totalSeconds.TotalSeconds == 0)
            {
                _timer.Stop();
                StartQuizRequestModel startQuizRequest = new StartQuizRequestModel()
                {
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    CourseQuizId = courseQuizId
                };
                var finalSumbitQuizResponse = await FinalSubmitAPI(startQuizRequest);
                if (finalSumbitQuizResponse != null && finalSumbitQuizResponse.StatusCode == 200)
                {
                    IsVisiblePreviosBtn = false;
                    IsVisibleNextBtn = false;
                    IsVisbleTimeOverFrame = true;
                    await Task.Delay(1000);
                    IsVisbleSuccessFrame = false;
                    Application.Current.MainPage = new QuizResultPage();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", finalSumbitQuizResponse.Message, "Ok");
                }
                //do something after hitting 0, in this example it just stops/resets the timer
                //StopTimerCommand();
            }
            else
            {
                TotalSeconds = _totalSeconds.Subtract(new TimeSpan(0, 0, 0, 1));
            }
        }
        #endregion
    }
}
