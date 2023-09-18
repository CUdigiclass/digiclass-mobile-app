using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPage : ContentPage
    {
        public ResetPasswordPage()
        {
            InitializeComponent();
        }
        private void BorderlessEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            vm.PasswordImage = !string.IsNullOrEmpty(vm.ConfirmPassword) ? "showPasswordIcon" : "PasswordIcon";
            //vm.IsPassword = true;
        }

        private void SendOTP_Tapped(object sender, EventArgs e)
        {

        }
    }
}