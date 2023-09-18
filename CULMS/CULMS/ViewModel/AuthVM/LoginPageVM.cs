using CULMS.Helpers;
using CULMS.Helpers.APIHelper;
using CULMS.Model;
using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using CULMS.View.Auth;
using CULMS.View.Dashboard;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CULMS.ViewModel.AuthVM
{
    public class LoginPageVM : ObservableObject
    {
        #region Private Properties

        private bool isPassword;
        private string passwordImage;
        private string password;
        private bool isVisibleTxtEmailError;
        private string msgEmailError;
        private string email;
        private string token;
        #endregion

        #region Public Properties
        private string deviceId;
        private bool isMobileDevice;

        public bool IsMobileDevice
        {
            get { return isMobileDevice; }
            set { isMobileDevice = value; OnPropertyChanged(nameof(IsMobileDevice)); }
        }

        public string DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; OnPropertyChanged(nameof(DeviceId)); }
        }

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(nameof(Email)); }
        }


        //public string Email
        //{
        //    get { return email; }
        //    set
        //    {
        //        email = value;
        //        if (email != null)
        //        {
        //            IsVisibleTxtEmailError = false;
        //        }
        //    }
        //}

        public string MsgEmailError
        {
            get { return msgEmailError; }
            set { msgEmailError = value; OnPropertyChanged(nameof(MsgEmailError)); }
        }

        public bool IsVisibleTxtEmailError
        {
            get { return isVisibleTxtEmailError; }
            set { isVisibleTxtEmailError = value; OnPropertyChanged(nameof(IsVisibleTxtEmailError)); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(nameof(Password)); }
        }

        public string PasswordImage
        {
            get { return passwordImage; }
            set { passwordImage = value; OnPropertyChanged(nameof(PasswordImage)); }
        }

        public bool IsPassword
        {
            get { return isPassword; }
            set { isPassword = value; OnPropertyChanged(nameof(IsPassword)); }
        }


        public string Token
        {
            get { return token; }
            set { token = value; OnPropertyChanged(nameof(Token)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Constructor to login page
        /// </summary>
        public LoginPageVM()
        {
            PasswordImage = "hidePasswordIcon";
            IsPassword = true;
            //DeviceId = DependencyService.Get<IDependencyDemo>().GetThePlatformMessage();
            //DeviceId = DependencyService.Get<IDependencyDemo>().GetThePlatformMessage();
        }
        private bool SignInClicked()
        {
            bool IsValid = true;
            try
            {
                StringConstant.IsValidEmail = Preferences.Get("ValidEmail", false);
                if (string.IsNullOrWhiteSpace(Email))
                {
                    IsVisibleTxtEmailError = true;
                    MsgEmailError = "*Enter Email";
                    IsValid = false;
                }
                else if (StringConstant.IsValidEmail != true)
                {
                    IsVisibleTxtEmailError = true;
                    MsgEmailError = "Please enter valid email id";
                    IsValid = false;
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Please enter password", "Ok");
                    IsValid = false;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return IsValid;
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command for navigate to back page
        /// </summary>
        public Command FogotPasswordCommand => new Command(async () =>
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new ForgotPasswordPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });

        /// <summary>
        /// Command for show and hide password
        /// </summary>
        public Command ShowPasswordCommand => new Command(() =>
        {
            try
            {
                if (PasswordImage == "hidePasswordIcon")
                {
                    IsPassword = false;
                    PasswordImage = "showPasswordIcon";
                }
                else if (PasswordImage == "showPasswordIcon")
                {
                    IsPassword = true;
                    PasswordImage = "hidePasswordIcon";
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });

        /// <summary>
        /// Login command
        /// </summary>
        public Command LoginCommand => new Command(async () =>
        {
            try
            {
                IsLoading = true;
                await Task.Delay(1);
                //if (SignInClicked())
                //{
                LoginRequestModel loginRequestModel = new LoginRequestModel()
                {
                    UserId = Email,
                    Password = Password,
                    DeviceId = Preferences.Get("FCMToken", string.Empty),
                    IsMobileDevice = true
                };
                var response = await LoginAPI(loginRequestModel);
                if (response != null && response.StatusCode == 200)
                {
                    Token = response.Data.Token;
                    var time1 = DateTime.Now;
                    Preferences.Set("LoginTime", time1.ToString());
                    Preferences.Set("TokenExpireTime", response.Data.Expires_In);
                    Preferences.Set("Token", response.Data.Token);
                    ValidateTokenMethod();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
                }
                //}
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

        GoogleUser GoogleUser = new GoogleUser();

        public void LoginFromGoogle()
        {
            try
            {
                //GoogleUser = googleUser;
                Preferences.Set(StringConstant.TabIdKey, 1);
                Preferences.Set(StringConstant.IsLogin, true);
                Application.Current.MainPage = new HomePage();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        private async void ValidateTokenMethod()
        {
            try
            {
                //Device.BeginInvokeOnMainThread(() =>
                //{
                //    IsLoading = true;
                //Task.Delay(100000);
                //});
                //var tokenValidateresponse = await ValidateTokenApi();
                var tokenValidateresponse = await ValidateTokenApi();
                Utilities.SavedProfileData = tokenValidateresponse;
                if (tokenValidateresponse.Data != null && tokenValidateresponse.Message == "Ok")
                {

                    Preferences.Set(StringConstant.TabIdKey, 1);
                    Preferences.Set(StringConstant.IsLogin, true);
                    Preferences.Set(StringConstant.UserId, tokenValidateresponse.Data.UserId);
                    Preferences.Set(StringConstant.RoleId, tokenValidateresponse.Data.RoleId);
                    Application.Current.MainPage =new NavigationPage( new HomePage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", tokenValidateresponse.Message, "Ok");
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                //Device.BeginInvokeOnMainThread(() =>
                //{
                //IsLoading = false;
                //Task.Delay(1000);
                //});
            }
        }
        #endregion

        #region API Methods

        //public async Task<AuthResponseObject<TokenValidateData>> ValidateTokenApi()
        //{
        //    AuthResponseObject<TokenValidateData> validateresponse = new AuthResponseObject<TokenValidateData>();
        //    try
        //    {
        //        APIService api = new APIService();
        //        validateresponse = await api.TokenValidateAsync(Token);
        //    }
        //    catch (Exception ex)
        //    {
        //        Crashes.TrackError(ex);
        //    }
        //    return validateresponse;
        //}


        public async Task<LoginResponseModel> LoginAPI(LoginRequestModel attendanceRequest)
        {
            LoginResponseModel loginResponse = new LoginResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(attendanceRequest);
                var loginUrl = StringConstant.ApiKeyUrl + StringConstant.ApeKeyLogin;
                var apiResponse = await aPICall.PostRequest(loginUrl, jsonRequest).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return loginResponse;
        }
        public async Task<ValidateTokenResponseModel> ValidateTokenApi()
        {
            ValidateTokenResponseModel validateresponse = new ValidateTokenResponseModel();
            try
            {
                APICall aPICall = new APICall();
                var loginUrl = StringConstant.ApiKeyUrl + StringConstant.ApeKeyValidateToken + Token;
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
