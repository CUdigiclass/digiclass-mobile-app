using System;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        private static Timer _timer;
        private double _shotClock;
        private DateTime _startTime;
        public ForgotPasswordPage()
        {
            InitializeComponent();
            vm.UpdateTimerEvent += Vm_UpdateTimerEvent;
        }

        private void Vm_UpdateTimerEvent()
        {
            TimerMethod();
        }

        private void TimerMethod()
        {
            _timer = new Timer();
            _startTime = DateTime.UtcNow;
            _timer.Start();
            _timer.Interval = 30;
            _timer.Elapsed += OnTimedEvent;
            _shotClock = 30.00;
        }

        private void BtnPause_Clicked(object sender, EventArgs e)
        {
            _timer.Stop();
            var elapsedSinceBtnPausePressed = (DateTime.UtcNow - _startTime).TotalSeconds;
            _shotClock -= elapsedSinceBtnPausePressed;
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            var elapsedSinceBtnStartPressed = (DateTime.UtcNow - _startTime).TotalSeconds;
            var remaining = _shotClock - elapsedSinceBtnStartPressed;
            Device.BeginInvokeOnMainThread(() =>
            {
                if (remaining > 1.00)
                {
                    lblTimer.Text = Math.Floor(remaining).ToString();
                }
                //else if (remaining <= 1.00 && remaining > 0.00)
                //{
                //    lblTimer.Text = Math.Round(remaining, 2).ToString();
                //}
                else
                {
                    lblTimer.Text = "0";
                    _timer.Stop();
                }
            });
        }
        private void ResenOtpClicked(object sender, EventArgs e)
        {
            if (lblTimer.Text == "0")
            {
                vm.ResendOTPMethod();
                _startTime = DateTime.UtcNow;
                _timer.Start();
            }
        }
        private async void SendOTP_Tapped(object sender, EventArgs e)
        {
            await SendOTPFrame.ScaleTo(0.75, 100);
            await SendOTPFrame.ScaleTo(1, 100);
        }

        private void OTP1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OTP1.Text))
            {
                OTP2.Focus();
            }
            else
            {
                OTP1.Focus();
            }
        }

        private void OTP2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OTP2.Text))
            {
                OTP3.Focus();
            }
            else
            {
                OTP1.Focus();
            }
        }

        private void OTP3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OTP3.Text))
            {
                OTP4.Focus();
            }
            else
            {
                OTP2.Focus();
            }
        }

        private void OTP4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(OTP4.Text))
            {
                OTP3.Focus();
            }
        }

        private async void VerifyOTP_Tapped(object sender, EventArgs e)
        {
            await VerifyOTPFrame.ScaleTo(0.75, 100);
            await VerifyOTPFrame.ScaleTo(1, 100);
        }
    }
}