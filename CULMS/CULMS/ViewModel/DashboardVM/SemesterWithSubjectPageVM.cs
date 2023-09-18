using CULMS.Helpers;
using CULMS.Helpers.APIHelper;
using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;
using System;
using Xamarin.Essentials;
using System.Collections.ObjectModel;

namespace CULMS.ViewModel.DashboardVM
{
    public class SemesterWithSubjectPageVM : ObservableObject
    {
        #region Private Properties

        private ObservableCollection<CourseSemesterSubject> semesterWithSubjectList;
        private string semesterName;

        #endregion

        #region Public Properties

        public string SemesterName
        {
            get { return semesterName; }
            set { semesterName = value; OnPropertyChanged(nameof(SemesterName)); }
        }

        public ObservableCollection<CourseSemesterSubject> SemesterWithSubjectList
        {
            get { return semesterWithSubjectList; }
            set { semesterWithSubjectList = value; OnPropertyChanged(nameof(SemesterWithSubjectList)); }
        }

        #endregion

        #region Methods
        public SemesterWithSubjectPageVM()
        {
            //SemesterWithSubjectAPIMethod();
        }

        public async void SemesterWithSubjectAPIMethod(int id)
        {
            try
            {
                SemeterWithSubjectRequestModel request = new SemeterWithSubjectRequestModel()
                {
                    CourseId = id
                };
                var response = await GetSemesterWithSubjectAPI(request);
                if (response != null && response.StatusCode == 200)
                {
                    SemesterName = response.Data.CourseSemester.FirstOrDefault().SemesterName;
                    SemesterWithSubjectList = new ObservableCollection<CourseSemesterSubject>(response.Data.CourseSemesterSubject.Select(data => new CourseSemesterSubject()
                    {
                        SubjectName = data.SubjectName,
                        StreamName = data.StreamName
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

        #endregion

        #region API Method

        public async Task<SemeterWithSubjectResponseModel> GetSemesterWithSubjectAPI(SemeterWithSubjectRequestModel semeterWithSubjectRequest)
        {
            SemeterWithSubjectResponseModel withSubjectResponseModel = new SemeterWithSubjectResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(semeterWithSubjectRequest);
                var courseUrl = StringConstant.ApiKeyUrl + StringConstant.ApiKeySemesterWithSubject;
                var apiResponse = await aPICall.PostRequestToken(courseUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        withSubjectResponseModel = JsonConvert.DeserializeObject<SemeterWithSubjectResponseModel>(apiReponseData);
                     
                    
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return withSubjectResponseModel;
        }
        #endregion
    }
}
