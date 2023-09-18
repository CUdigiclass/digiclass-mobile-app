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
    public class LiveClassRecodingPageVM : ObservableObject
    {
        #region Private Properties

        #endregion

        #region Public Properties
        private List<LiveClassRecordingData> recordingList;

        public List<LiveClassRecordingData> RecordingList
        {
            get { return recordingList; }
            set { recordingList = value; OnPropertyChanged(nameof(RecordingList)); }
        }
        private List<LiveClassRecordingData> data;

        public List<LiveClassRecordingData> Data
        {
            get { return data; }
            set { data = value; OnPropertyChanged(nameof(Data)); }
        }
        #endregion

        #region Methods

        public LiveClassRecodingPageVM()
        {
            //RecordingLiveClass();
            Data = new List<LiveClassRecordingData>();
        }

        public async void RecordingLiveClass(int pageno)
        {
            try
            {
                LiveClassRecordingRequestModel liveClassRecordingRequest = new LiveClassRecordingRequestModel()
                {
                    CourseId = Utilities.courseId,
                    PageNo = pageno,
                    PageSize = 10,
                    RoleId = Preferences.Get(StringConstant.RoleId, 0),
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                };
                var response = await LiveRecordingAPI(liveClassRecordingRequest);
                if (response != null && response.statusCode == 200)
                {
                    RecordingList = new List<LiveClassRecordingData>(response.data.Select(x => new LiveClassRecordingData()
                    {
                        Topic = x.Topic,
                        FileUrl = x.FileUrl,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                        LiveDate = x.LiveDate,
                    }));
                    var d = RecordingList;
                    Data.AddRange(d);
                    RecordingList = Data;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Commands

        public Command LiveClassCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as LiveClassRecordingData;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new VideoPage(data.FileUrl));
                await RichNavigation.PushAsync(new VideoPage(data.FileUrl), typeof(VideoPage));
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

        public async Task<LiveClassRecordingResponseModel> LiveRecordingAPI(LiveClassRecordingRequestModel liveClassRecordingRequest)
        {
            LiveClassRecordingResponseModel newsResponseModel = new LiveClassRecordingResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(liveClassRecordingRequest);
                var url = StringConstant.ApiKeyUrl + StringConstant.ApiKeyLiveRecording;
                var apiResponse = await aPICall.PostRequestToken(url, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        newsResponseModel = JsonConvert.DeserializeObject<LiveClassRecordingResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return newsResponseModel;
        }
        #endregion
    }
}
