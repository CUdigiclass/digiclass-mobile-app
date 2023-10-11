using CULMS.Helpers;
using CULMS.Helpers.APIHelper;
using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using CULMS.View.Auth;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CULMS.ViewModel.AuthVM
{
    public class ResetPasswordPageVM : ObservableObject
    {

        #region Private Properties
        private string passwordImage;
        private string confirmPassword;
        private bool isPassword;
        private string newPassword;
        private bool isVisiblepassword_change_Popup;
        #endregion

        #region Public Properties

        private string oldPassword;
        private string passwordErrorMsg;

        public string PasswordErrorMsg
        {
            get { return passwordErrorMsg; }
            set { passwordErrorMsg = value; OnPropertyChanged(nameof(PasswordErrorMsg)); }
        }

        public string OldPassword
        {
            get { return oldPassword; }
            set { oldPassword = value; OnPropertyChanged(nameof(OldPassword)); }
        }

        public bool IsVisiblepassword_change_Popup
        {
            get { return isVisiblepassword_change_Popup; }
            set { isVisiblepassword_change_Popup = value; OnPropertyChanged(nameof(IsVisiblepassword_change_Popup)); }
        }

        public string NewPassword
        {
            get { return newPassword; }
            set { newPassword = value; OnPropertyChanged(nameof(NewPassword)); }
        }

        public bool IsPassword
        {
            get { return isPassword; }
            set { isPassword = value; OnPropertyChanged(nameof(IsPassword)); }
        }

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); }
        }
        public string PasswordImage
        {
            get { return passwordImage; }
            set { passwordImage = value; OnPropertyChanged(nameof(PasswordImage)); }
        }
        #endregion

        #region Methods

        public ResetPasswordPageVM()
        {
            PasswordImage = "PasswordIcon";
            IsPassword = true;
            PasswordErrorMsg = "Password should contain a lowecase letter, a capital(uppercase) letter, a number, a special character and length between 6 to 13 characters.";
        }
        #endregion

        #region Commands

        /// <summary>
        /// This command is used for show and hide password
        /// </summary>
        public Command ShowPasswordCommand => new Command(() =>
        {
            try
            {
                if (PasswordImage == "showPasswordIcon")
                {
                    IsPassword = false;
                    PasswordImage = "hidePasswordIcon";
                }
                else if (PasswordImage == "hidePasswordIcon")
                {
                    IsPassword = true;
                    PasswordImage = "showPasswordIcon";
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });

        /// <summary>
        /// This command is used for send request to reset password api
        /// </summary>
        public Command ResetPasswordCommand => new Command(async (o) =>
        {
            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmPassword))
                {
                    if (NewPassword == ConfirmPassword)
                    {
                        ChangePasswordRequestModel changePasswordRequest = new ChangePasswordRequestModel()
                        {
                            OldPassword = OldPassword,
                            Password = ConfirmPassword,
                            UserId = Preferences.Get(StringConstant.UserId, string.Empty)
                        };
                        if (StringConstant.IsPasswordIsValid)
                        {
                            var response = await ChangePasswordAPI(changePasswordRequest);
                            if (response != null && response.StatusCode == 200)
                            {
                                await Task.Delay(1000);
                                IsVisiblepassword_change_Popup = true;
                                await Task.Delay(2000);
                                Preferences.Set(StringConstant.IsLogin, false);
                                Preferences.Set("Token", null);
                                Preferences.Set(StringConstant.UserId, string.Empty);
                                Preferences.Set(StringConstant.RoleId, 0);
                                Preferences.Set("FCMToken", string.Empty);
                                App.Current.MainPage = new LoginPage();
                                StringConstant.IsPasswordIsValid = false;
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
                            }
                        }
                        else
                        {                            
                            Frame ResetPasswordBtn = (Frame)o;
                            //await ResetPasswordBtn.TranslateTo(50, 0, 400);
                            ResetPasswordBtn.Scale = 0.75;
                            await Task.Delay(1000);
                            ResetPasswordBtn.Scale = 1;
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Password not matched.", "Ok");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Enter password", "Ok");
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

        #region API Method

        public async Task<ChangePasswordResponseModel> ChangePasswordAPI(ChangePasswordRequestModel changePasswordRequest)
        {
            ChangePasswordResponseModel responseModel = new ChangePasswordResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(changePasswordRequest);
                var url = StringConstant.ApiKeyUrl + StringConstant.ApiKeyChangePassword;
                var apiResponse = await aPICall.PostRequestToken(url, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        responseModel = JsonConvert.DeserializeObject<ChangePasswordResponseModel>(apiReponseData);
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
