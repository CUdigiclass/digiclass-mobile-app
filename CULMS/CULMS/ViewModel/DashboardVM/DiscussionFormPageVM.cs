
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
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials; 
using Xamarin.Forms;

namespace CULMS.ViewModel.DashboardVM
{
    public class DiscussionFormPageVM : ObservableObject
    {
        #region Private Properties

        #endregion

        #region Public Properties

        private List<DiscussionQuestionListData> discussionQuestionList;
        private List<DiscussionQuestionListData> data;

        public List<DiscussionQuestionListData> Data
        {
            get { return data; }
            set { data = value; OnPropertyChanged(nameof(Data)); }
        }
        public List<DiscussionQuestionListData> DiscussionQuestionList
        {
            get { return discussionQuestionList; }
            set { discussionQuestionList = value; OnPropertyChanged(nameof(DiscussionQuestionList)); }
        }

        #endregion

        #region Methods
        public DiscussionFormPageVM()
        {
            Data = new List<DiscussionQuestionListData>();
        }

        public async void DiscussionQuestionMethod(int configurationId, int pageno)
        {
            try
            {
                IsLoading = true;                
                await Task.Delay(50);
                DiscussionQuestionListRequestModel requestModel = new DiscussionQuestionListRequestModel()
                {
                    CourseId = Utilities.courseId.ToString(),
                    PageNo = pageno,
                    PageSize = 10,
                    RoleId = Preferences.Get(StringConstant.RoleId, 0),
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                };
                var response = await DiscussionQuestionListAPI(requestModel);
                if (response != null && response.StatusCode == 200)
                {
                    DiscussionQuestionList = new List<DiscussionQuestionListData>(response.Data.Select(x => new DiscussionQuestionListData()
                    {
                        Title = x.Title,
                        Question = x.Question,
                        Id = x.Id,
                        CourseId = x.CourseId,
                        DiscussionDoc = x.DiscussionDoc
                    }));
                    var d = DiscussionQuestionList;
                    Data.AddRange(d);
                    DiscussionQuestionList = Data;
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

        public Command DiscussionListCommand => new Command(async (param) =>
        {
            try
            {
                var data = param as DiscussionQuestionListData;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new DiscussionQuestionReplyPage(data));
                await RichNavigation.PushAsync(new DiscussionQuestionReplyPage(data), typeof(DiscussionQuestionReplyPage));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });
        #endregion

        #region API Methods

        public async Task<DiscussionQuestionListResponseModel> DiscussionQuestionListAPI(DiscussionQuestionListRequestModel discussionQuestionListRequest)
        {
            DiscussionQuestionListResponseModel questionListResponseModel = new DiscussionQuestionListResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(discussionQuestionListRequest);
                var discussionQuestionURL = StringConstant.ApiKeyUrl + StringConstant.ApiKeyDiscussionQuestionList;
                var apiResponse = await aPICall.PostRequestToken(discussionQuestionURL, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        questionListResponseModel = JsonConvert.DeserializeObject<DiscussionQuestionListResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return questionListResponseModel;
        }
        #endregion
    }
}
