using CULMS.Helpers;
using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using Microsoft.AppCenter.Crashes;
using System.Collections.ObjectModel;
using System;
using CULMS.Helpers.APIHelper;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Linq;
using Xamarin.Forms;
using CULMS.View.Dashboard;
using CULMS.Services;

namespace CULMS.ViewModel.DashboardVM
{
    public class MessagePageVM : ObservableObject
    {
        #region Private Properties

        private ObservableCollection<Global> announcementList;
        private ObservableCollection<Course> courseAnnouncementList;
        private ObservableCollection<Stream> streamAnnouncementList;
        #endregion

        #region Public Properties


        public ObservableCollection<Stream> StreamAnnouncementList
        {
            get { return streamAnnouncementList; }
            set { streamAnnouncementList = value; OnPropertyChanged(nameof(StreamAnnouncementList)); }
        }

        public ObservableCollection<Course> CourseAnnouncementList
        {
            get { return courseAnnouncementList; }
            set { courseAnnouncementList = value; OnPropertyChanged(nameof(CourseAnnouncementList)); }
        }

        public ObservableCollection<Global> AnnouncementList
        {
            get { return announcementList; }
            set { announcementList = value; OnPropertyChanged(nameof(AnnouncementList)); }
        }


        #endregion

        #region Methods

        public MessagePageVM()
        {
            AnnouncementMethod();
            CourseAnnouncementMethod();
            StreamAnnouncementMethod();
        }

        private async void StreamAnnouncementMethod()
        {
            try
            {
                AnnouncementRequestModel announcementRequestModel = new AnnouncementRequestModel()
                {
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    RoleId = Preferences.Get(StringConstant.RoleId, 1)
                };
                var response = await AnnouncementAPI(announcementRequestModel);
                if (response != null && response.StatusCode == 200)
                {
                    StreamAnnouncementList = new ObservableCollection<Stream>(response.Data.Stream.Select(data => new Stream()
                    {
                        Announcements = data.Announcements,
                        FileName = data.FileName,
                        FileURL = data.FileURL,
                        AnnouncementType = "Program Announcement",
                        PDFIcon = "PDFIcon.png"
                    }));
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async void CourseAnnouncementMethod()
        {
            try
            {
                AnnouncementRequestModel announcementRequestModel = new AnnouncementRequestModel()
                {
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    RoleId = Preferences.Get(StringConstant.RoleId, 1)
                };
                var response = await AnnouncementAPI(announcementRequestModel);
                if (response != null && response.StatusCode == 200)
                {
                    CourseAnnouncementList = new ObservableCollection<Course>(response.Data.Course.Select(data => new Course()
                    {
                        Announcements = data.Announcements,
                        FileName = data.FileName,
                        FileURL = data.FileURL,
                        AnnouncementType = "Course Announcement",
                        PDFIcon = "PDFIcon.png"
                    }));
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async void AnnouncementMethod()
        {
            try
            {
                AnnouncementRequestModel announcementRequestModel = new AnnouncementRequestModel()
                {
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty),
                    RoleId = Preferences.Get(StringConstant.RoleId, 1)
                };
                var response = await AnnouncementAPI(announcementRequestModel);
                if (response != null && response.StatusCode == 200)
                {
                    AnnouncementList = new ObservableCollection<Global>(response.Data.Global.Select(data => new Global()
                    {
                        Announcements = data.Announcements,
                        FileName = data.FileName,
                        FileURL = data.FileURL,
                        AnnouncementType = "Global Announcement",
                        PDFIcon = "PDFIcon.png"
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

        public Command GlobalAnnouncementPDFCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as Global;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new NewPDFView(data.FileURL));
                await RichNavigation.PushAsync(new NewPDFView(data.FileURL),typeof(NewPDFView));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });
        public Command CourseAnnouncementPDFCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as Course;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new NewPDFView(data.FileURL));
                await RichNavigation.PushAsync(new NewPDFView(data.FileURL), typeof(NewPDFView));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });
        public Command StreamAnnouncementPDFCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                var data = param as Stream;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new NewPDFView(data.FileURL));
                await RichNavigation.PushAsync(new NewPDFView(data.FileURL), typeof(NewPDFView));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });
        #endregion

        #region API Methods

        public async Task<AnnouncementResponseModel> AnnouncementAPI(AnnouncementRequestModel announcementRequest)
        {
            AnnouncementResponseModel announcementResponse = new AnnouncementResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(announcementRequest);
                var courseUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeyAnnouncement;
                var apiResponse = await aPICall.PostRequestToken(courseUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        announcementResponse = JsonConvert.DeserializeObject<AnnouncementResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return announcementResponse;
        }
        #endregion
    }
}
