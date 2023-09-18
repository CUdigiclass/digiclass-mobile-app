using CULMS.Model;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IGoogleManager _googleManager;
        GoogleUser GoogleUser = new GoogleUser();
        public bool IsLogedIn { get; set; }
        public LoginPage()
        {

            _googleManager = DependencyService.Get<IGoogleManager>();
            //CheckUserLoggedIn();
            InitializeComponent();
        }
        private void CheckUserLoggedIn()
        {
            _googleManager.Login(OnLoginComplete);
        }


        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            _googleManager.Login(OnLoginComplete);
        }
        private void OnLoginComplete(GoogleUser googleUser, string message)
        {
            try
            {
                if (googleUser != null)
                {
                    GoogleUser = googleUser;
                    txtName.Text = GoogleUser.Name;
                    txtEmail.Text = GoogleUser.Email;
                    imgProfile.Source = GoogleUser.Picture;
                    bool IsCumail = GoogleUser.Email.Contains("cumail.in");
                    if (IsCumail)
                    {
                        vm.LoginFromGoogle();
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Message", "Only CU mail will be login", "Ok");
                        GoogleLogout();
                        txtName.Text = "Name :";
                        txtEmail.Text = "Email: ";
                        imgProfile.Source = "";
                    }
                    IsLogedIn = true;
                }
                else
                {
                    DisplayAlert("Message", message, "Ok");
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        private void GoogleLogout()
        {
            _googleManager.Logout();
            IsLogedIn = false;
        }
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            _googleManager.Logout();

            txtName.Text = "Name :";
            txtEmail.Text = "Email: ";
            imgProfile.Source = "";
        }
        //private async void LoginFrame_Tapped(object sender, EventArgs e)
        //{
        //    await LoginFrame.ScaleTo(0.75, 100);
        //    await LoginFrame.ScaleTo(1, 100);
        //}

        //private async void ForgotPassword_Clicked(object sender, EventArgs e)
        //{
        //    await ForgotPasswordLabel.ScaleTo(0.75, 100);
        //    await ForgotPasswordLabel.ScaleTo(1, 100);
        //}
    }
}