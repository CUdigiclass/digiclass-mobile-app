using CULMS.Helpers;
using CULMS.View.Auth;
using CULMS.View.Dashboard;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace CULMS
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var isLogin = Preferences.Get(StringConstant.IsLogin, false);
            int selectedtabbed = Preferences.Get(StringConstant.TabIdKey, 0);
            if (isLogin)
            {
                switch (selectedtabbed)
                {
                    case 1:
                        Preferences.Set(StringConstant.TabIdKey, 1);
                        MainPage = new NavigationPage(new HomePage());
                        break;
                    case 2:
                        Preferences.Set(StringConstant.TabIdKey, 2);
                        MainPage = new NavigationPage(new NewsPage());
                        break;
                    case 3:
                        Preferences.Set(StringConstant.TabIdKey, 3);
                        MainPage = new NavigationPage(new ProfilePage());
                        break;
                    case 4:
                        Preferences.Set(StringConstant.TabIdKey, 4);
                        MainPage = new NavigationPage(new MessagePage());
                        break;
                }
                //MainPage = new NewVideoPage();
            }
            else
            {
                 MainPage = new LoginPage();
                //MainPage = new NewVideoPage();
            }
        }

        protected override void OnStart()
        {
            //AppCenter.Start("android=1b0c088d-6419-4848-b1a2-541b92162ec3;" + "ios=a58be42c-f399-420a-bd2e-33498f61f9e6;", typeof(Analytics), typeof(Crashes));
            try
            {
                var da = Preferences.Get("LoginTime", string.Empty);
                if (!string.IsNullOrEmpty(da))
                {
                    var loginTime = Convert.ToDateTime(Preferences.Get("LoginTime", string.Empty));
                    var currentTime = DateTime.Now;
                    var token_expire_time = Preferences.Get("TokenExpireTime", 0);
                    var abc = loginTime.AddMinutes(token_expire_time);
                    if (currentTime >= abc)
                    {
                        LogoutMethod();
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        protected override void OnResume()
        {
            try
            {
                var da = Preferences.Get("LoginTime", string.Empty);
                if (!string.IsNullOrEmpty(da))
                {
                    var loginTime = Convert.ToDateTime(Preferences.Get("LoginTime", string.Empty));
                    var currentTime = DateTime.Now;
                    var token_expire_time = Preferences.Get("TokenExpireTime", 0);
                    var abc = loginTime.AddMinutes(token_expire_time);
                    if (currentTime >= abc)
                    {
                        LogoutMethod();
                    }
                    //if (currentTime >= loginTime.AddMinutes(Utilities.TotalLogoutTime))
                    //{
                    //    //LogoutMethod();
                    //}
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        private async void LogoutMethod()
        {
            try
            {
                await Task.Delay(50);
                Preferences.Set(StringConstant.IsLogin, false);
                Preferences.Set("Token", null);
                Preferences.Set(StringConstant.UserId, string.Empty);
                Application.Current.MainPage = new LoginPage();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        protected override void OnSleep()
        {
        }

    }
}
