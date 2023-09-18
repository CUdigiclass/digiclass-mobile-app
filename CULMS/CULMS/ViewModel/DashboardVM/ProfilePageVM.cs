using CULMS.Helpers.APIHelper;
using CULMS.Helpers;
using CULMS.Model.ResponseModel;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using Xamarin.Essentials;

namespace CULMS.ViewModel.DashboardVM
{
    public class ProfilePageVM : ObservableObject
    {
        #region Private Properties

        private string firstName;
        private string lastName;
        private string emailId;
        private string gender;
        private string phoneNumber;
        #endregion

        #region Public Properties

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }
        public string Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged(nameof(Gender)); }
        }
        public string EmailId
        {
            get { return emailId; }
            set { emailId = value; OnPropertyChanged(nameof(EmailId)); }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(nameof(LastName)); }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        #endregion

        #region Methods
        public ProfilePageVM()
        {
            ProfileDetailMethod();
        }

        private async void ProfileDetailMethod()
        {
            try
            {
                var response = await ValidateTokenApi();              
                if (response != null && response.StatusCode == 200)
                {
                    FirstName = response.Data.FirstName;
                    LastName = response.Data.LastName;
                    Gender = response.Data.Gender;
                    PhoneNumber = response.Data.PhoneNumber;
                    EmailId = response.Data.UserId;
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

        #region API Methods
        public async Task<ValidateTokenResponseModel> ValidateTokenApi()
        {
            ValidateTokenResponseModel validateresponse = new ValidateTokenResponseModel();
            try
            {
                APICall aPICall = new APICall();
                var loginUrl = StringConstant.ApiKeyUrl + StringConstant.ApeKeyValidateToken + Preferences.Get("Token", ""); ;
                var apiResponse = await aPICall.GetRequest(loginUrl, string.Empty).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        validateresponse = JsonConvert.DeserializeObject<ValidateTokenResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return validateresponse;
        }
        #endregion
    }
}
