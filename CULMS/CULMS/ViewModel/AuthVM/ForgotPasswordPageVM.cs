using CULMS.Helpers;
using CULMS.View.Auth;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CULMS.ViewModel.AuthVM
{
    public class ForgotPasswordPageVM : ObservableObject
    {
        #region Private Properties
        public delegate void UpdateTimer();
        public event UpdateTimer UpdateTimerEvent;
        private string title;
        private bool isVisibleTxtEmailError;
        private string msgEmailError;
        private string email;
        private bool isVisibleOTPFrame;
        private string otp1;
        private string otp2;
        private string otp3;
        private string otp4;
        #endregion

        #region Public Properties



        public string Otp4
        {
            get { return otp4; }
            set { otp4 = value; OnPropertyChanged(nameof(Otp4)); }
        }

        public string Otp3
        {
            get { return otp3; }
            set { otp3 = value; OnPropertyChanged(nameof(Otp3)); }
        }

        public string Otp2
        {
            get { return otp2; }
            set { otp2 = value; OnPropertyChanged(nameof(Otp2)); }
        }

        public string Otp1
        {
            get { return otp1; }
            set { otp1 = value; OnPropertyChanged(nameof(Otp1)); }
        }

        public bool IsVisibleOTPFrame
        {
            get { return isVisibleOTPFrame; }
            set { isVisibleOTPFrame = value; OnPropertyChanged(nameof(IsVisibleOTPFrame)); }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                if (email != null)
                {
                    IsVisibleTxtEmailError = false;
                }
            }
        }

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

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(nameof(Title)); }
        }

        #endregion

        #region Methods
        public ForgotPasswordPageVM()
        {
            Title = "Forgot Password";
        }
        private bool SendOTPClicked()
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
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return IsValid;
        }

        /// <summary>
        /// This method is used for send request to resend OTP api
        /// </summary>
        public void ResendOTPMethod()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
            }
        }
        #endregion

        #region Commands
        public Command SendOTPCommand => new Command(() =>
        {
            try
            {
                if (SendOTPClicked())
                {
                    IsVisibleOTPFrame = true;
                    UpdateTimerEvent?.Invoke();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });

      

        public Command CloseOTPFrameCommand => new Command(() =>
        {
            try
            {
                IsVisibleOTPFrame = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });
        public Command VerifyOtpCommand => new Command(async () =>
        {
            try
            {
                if (!string.IsNullOrEmpty(Otp1) && !string.IsNullOrEmpty(Otp2) && !string.IsNullOrEmpty(Otp3) && !string.IsNullOrEmpty(Otp4))
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new ResetPasswordPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter OTP", "OK");
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });
        #endregion
    }
}
