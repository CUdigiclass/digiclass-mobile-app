using CULMS.Helpers;
using CULMS.Helpers.APIHelper;
using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using Xamarin.Essentials;
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using CULMS.View.Dashboard;
using CULMS.Services;

namespace CULMS.ViewModel.DashboardVM
{
    public class CourseModulePageVM : ObservableObject
    {
        #region Private Properties

        private ObservableCollection<Configuration> moduleList;
        private bool isVisibleModuleList;

        #endregion

        #region Public Properties

        private bool isVisibleQuadrantList;
        private ObservableCollection<Configuration> quadrantList;

        public ObservableCollection<Configuration> QuadrantList
        {
            get { return quadrantList; }
            set { quadrantList = value; OnPropertyChanged(nameof(QuadrantList)); }
        }

        public bool IsVisibleQuadrantList
        {
            get { return isVisibleQuadrantList; }
            set { isVisibleQuadrantList = value; OnPropertyChanged(nameof(IsVisibleQuadrantList)); }
        }

        public bool IsVisibleModuleList
        {
            get { return isVisibleModuleList; }
            set { isVisibleModuleList = value; OnPropertyChanged(nameof(IsVisibleModuleList)); }
        }

        public ObservableCollection<Configuration> ModuleList
        {
            get { return moduleList; }
            set { moduleList = value; OnPropertyChanged(nameof(ModuleList)); }
        }

        #endregion

        #region Methods

        public CourseModulePageVM()
        {
            IsVisibleModuleList = true;
            IsVisibleQuadrantList = false;
            ModuleConfigurationMethod();
        }

        private async void ModuleConfigurationMethod()
        {
            try
            {
                EnrollCourseRequestModel enrollCourseRequestModel = new EnrollCourseRequestModel()
                {
                    LearnerUserId = ""
                };
                var response = await ModuleConfigurationAPI(enrollCourseRequestModel);
                if (response.StatusCode == 200 && response.Data != null)
                {
                    ModuleList = new ObservableCollection<Configuration>();
                    var filterdata = response.Data.Where(x => x.ModuleId == 9 && x.IsActive == true).ToList();
                    foreach (var item in filterdata)
                    {
                        foreach (var item1 in item.Configurations)
                        {
                            ModuleList.Add(new Configuration()
                            {
                                Value = item1.Value.Replace("_", " "),
                                ModuleId = item1.ModuleId,
                                ConfigurationId = item1.ConfigurationId,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Commands
        public Command CourseDocumentCommamnd => new Command(async () =>
        {
            try
            {
                //await Application.Current.MainPage.Navigation.PushModalAsync(new LiveClassRecodingPage());
                // await Application.Current.MainPage.Navigation.PushModalAsync(new CourseDocumentsPage());
                await RichNavigation.PushAsync(new CourseDocumentsPage(), typeof(CourseDocumentsPage));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });

        public Command QuadrantCommand => new Command(async (param) =>
        {
            try
            {
                var data = param as Configuration;
                Utilities.ContentType = data.ConfigurationId;
                if (data != null && data.ConfigurationId == 48)
                {
                    // await Application.Current.MainPage.Navigation.PushModalAsync(new QuizListPage());
                    await RichNavigation.PushAsync(new QuizListPage(), typeof(QuizListPage));
                }
                else if (data != null && data.ConfigurationId == 28)
                {
                    //await Application.Current.MainPage.Navigation.PushModalAsync(new StudentOptionPage(data));
                    await RichNavigation.PushAsync(new StudentOptionPage(data), typeof(StudentOptionPage));
                }
                else
                {
                    //await Application.Current.MainPage.Navigation.PushModalAsync(new CourseContentPage());
                    await RichNavigation.PushAsync(new CourseContentPage(), typeof(CourseContentPage));
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });

        public Command ModuleCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                IsVisibleModuleList = false;
                IsVisibleQuadrantList = true;
                var data = param as Configuration;
                Utilities.courseModuleId = data.ConfigurationId;
                EnrollCourseRequestModel enrollCourseRequestModel = new EnrollCourseRequestModel()
                {
                    LearnerUserId = ""
                };
                var response = await ModuleConfigurationAPI(enrollCourseRequestModel);
                if (response.StatusCode == 200 && response.Data != null)
                {
                    QuadrantList = new ObservableCollection<Configuration>();
                    var filterdata = response.Data.Where(x => x.ModuleId == 6 && x.IsActive == true).ToList();
                    foreach (var item in filterdata)
                    {
                        foreach (var item1 in item.Configurations)
                        {
                            QuadrantList.Add(new Configuration()
                            {
                                Value = item1.Value.Replace("- ", " "),
                                ModuleId = item1.ModuleId,
                                ConfigurationId = item1.ConfigurationId,
                            });
                        }
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

        #region API Methods
        public async Task<ModuleConfigurationResponseModel> ModuleConfigurationAPI(EnrollCourseRequestModel getEnrolledCourseRequest)
        {
            ModuleConfigurationResponseModel moduleConfigurationResponse = new ModuleConfigurationResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(getEnrolledCourseRequest);
                var moduleConfiguration = StringConstant.ApiKeyUrl + StringConstant.ApiKeyModuleConfigurationt;
                var apiResponse = await aPICall.PostRequestToken(moduleConfiguration, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        moduleConfigurationResponse = JsonConvert.DeserializeObject<ModuleConfigurationResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return moduleConfigurationResponse;
        }

        #endregion
    }
}
