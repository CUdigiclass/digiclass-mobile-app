using CULMS.ViewModel.DashboardVM;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.PartialView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HamburgerMenu : ContentView
    {
        public HamburgerMenu()
        {
            InitializeComponent();
            HamburgerMenuView.TranslateTo(-500, 0, 500, Easing.CubicInOut);
            BlackGrid.IsVisible = false;
            MessagingCenter.Subscribe<HomePageVM>(this, "Translate", async (sender) =>
            {
                await HamburgerMenuView.TranslateTo(0, 0, 500, Easing.CubicInOut);
                BlackGrid.IsVisible = true;
            });
            VersionTxt.Text = VersionTracking.CurrentVersion;
        }
        /// <summary>
        /// Tap gesture to translate and hide Hamburger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            BlackGrid.IsVisible = false;
            await HamburgerMenuView.TranslateTo(-500, 0, 500, Easing.CubicInOut);
            MessagingCenter.Send(this, "Hide");

        }
    }
}