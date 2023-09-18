using CULMS.Helpers;
using CULMS.Model.ResponseModel;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using CULMS.Model.RequestModel;
using CULMS.Helpers.APIHelper;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;

namespace CULMS.ViewModel.DashboardVM
{
    public class DiscussionQuestionReplyPageVM : ObservableObject
    {
        #region Private Properties

        #endregion

        #region Public Properties

        private string title;
        private string question;
        private List<DiscussionDoc> discussionDocList;
        private string reply;
        private int courseId;
        private int discussionQuestionID;
        private ObservableCollection<DiscussionReplyListData> replyList;

        public ObservableCollection<DiscussionReplyListData> ReplyList
        {
            get { return replyList; }
            set { replyList = value; OnPropertyChanged(nameof(ReplyList)); }
        }

        public int DiscussionQuestionID
        {
            get { return discussionQuestionID; }
            set { discussionQuestionID = value; OnPropertyChanged(nameof(DiscussionQuestionID)); }
        }

        public int CourseId
        {
            get { return courseId; }
            set { courseId = value; OnPropertyChanged(nameof(CourseId)); }
        }

        public string Reply
        {
            get { return reply; }
            set { reply = value; OnPropertyChanged(nameof(Reply)); }
        }

        public List<DiscussionDoc> DiscussionDocList
        {
            get { return discussionDocList; }
            set { discussionDocList = value; OnPropertyChanged(nameof(DiscussionDocList)); }
        }

        public string Question
        {
            get { return question; }
            set { question = value; OnPropertyChanged(nameof(Question)); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(nameof(Title)); }
        }


        #endregion

        #region Methods
        public DiscussionQuestionReplyPageVM()
        {
            //TestMethod();

        }

        public async void GetReplyListMethod()
        {
            try
            {
                IsLoading = true;
                await Task.Delay(5);
                DiscussionReplyListRequestModel discussionReplyListRequest = new DiscussionReplyListRequestModel()
                {
                    Id = DiscussionQuestionID.ToString(),
                    CourseId = CourseId,
                    PageNo = 1,
                    PageSize = 100,
                    RoleId = Preferences.Get(StringConstant.RoleId, 0),
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty)
                };
                var response = await GetReplyListAPI(discussionReplyListRequest);
                if (response != null && response.StatusCode == 200)
                {
                    ReplyList = new ObservableCollection<DiscussionReplyListData>(response.Data.Select(data => new DiscussionReplyListData()
                    {
                        Reply = data.Reply,
                        UserName = data.UserName
                    }));
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
        public async void SendReplyMethod()
        {
            string response = await RegisterUserAsync(DiscussionQuestionID.ToString(), CourseId.ToString(), Reply);
        }
        public async void DiscussionQuestionData(DiscussionQuestionListData data = null)
        {
            try
            {
                IsLoading = true;
                await Task.Delay(5);
                Title = data.Title;
                Question = data.Question;
                CourseId = data.CourseId;
                DiscussionQuestionID = data.Id;
                DiscussionDocList = data.DiscussionDoc;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally { IsLoading = false; }
        }
        #endregion

        #region Commands
        public Command SendReplyCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                await Task.Delay(5);
                var data = param as DiscussionQuestionReplyPageVM;
                if (data.Reply != string.Empty && data.Reply != null)
                {
                    SendReplyMethod();
                    await Task.Delay(1000);
                    GetReplyListMethod();
                    Reply = string.Empty;
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


        public async Task<DiscussionReplyListResponseModel> GetReplyListAPI(DiscussionReplyListRequestModel getEnrolledCourseRequest)
        {
            DiscussionReplyListResponseModel allCoursesResponse = new DiscussionReplyListResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(getEnrolledCourseRequest);
                var courseUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeyGetDiscussionReplyList;
                var apiResponse = await aPICall.PostRequestToken(courseUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        allCoursesResponse = JsonConvert.DeserializeObject<DiscussionReplyListResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return allCoursesResponse;
        }

        public async Task<string> RegisterUserAsync(string discussionQuestionId, string courseId, string reply)
        {
            string responseBody = null;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("Token", null));
                    httpClient.BaseAddress = new Uri(StringConstant.ApiKeyUrl);
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    form.Add(new StringContent(discussionQuestionId), "discussionQuestionId");
                    form.Add(new StringContent(courseId), "courseId");
                    form.Add(new StringContent(reply), "reply");
                    HttpResponseMessage response = await httpClient.PostAsync("DiscussionForum/AddDiscussionReply", form);
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseBody;
        }
        #endregion

    }
}
