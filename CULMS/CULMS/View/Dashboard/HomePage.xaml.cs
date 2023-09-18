using CULMS.Model;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        GoogleUser GoogleUser = new GoogleUser();
        private readonly IGoogleManager _googleManager;
        public bool IsLogedIn { get; set; }
        public HomePage()
        {
            //if()
            //{
            //    //GoogleUser = googleUser;
            //    //vm.UserData(googleUser);
            //    //txtName.Text = googleUser.Name;
            //    //txtEmail.Text = googleUser.Email;
            //    //imgProfile.Source = googleUser.Picture;
            //}
            _googleManager = DependencyService.Get<IGoogleManager>();
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            vm.GetCourseMethod();
        }
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            try
            {
                _googleManager.Logout();
                IsLogedIn = false;
                txtName.Text = "Name :";
                txtEmail.Text = "Email: ";
                imgProfile.Source = "";
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

    }
}