using CULMS.Helpers;
using CULMS.View.Auth;
using Microsoft.AppCenter.Crashes;
using System;
using System.Threading.Tasks;
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
        public Command ResetPasswordCommand => new Command(async () =>
        {
            try
            {
                if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmPassword))
                {

                    if (NewPassword == ConfirmPassword)
                    {
                        await Task.Delay(1000);
                        IsVisiblepassword_change_Popup = true;
                        await Task.Delay(2000);
                        App.Current.MainPage = new LoginPage();
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

            }
        });
        #endregion
    }
}
