using Microsoft.AppCenter.Crashes;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CULMS.Services
{
    public class RichNavigation
    {
        public static async Task PushAsync(Page page, Type type, bool isAnimated = true)
        {
            var i = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (i == null)
            {
                await App.Current.MainPage.Navigation.PushAsync(page, isAnimated);
                return;
            }
            if (i.GetType() != type)
            {
                await App.Current.MainPage.Navigation.PushAsync(page, isAnimated);
                return;
            }
        }
        public static async Task PushModelAysnc(Page page, Type type, bool isAnimated = true)
        {
            var i = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (i == null)
            {
                await App.Current.MainPage.Navigation.PushModalAsync(page, isAnimated);
                return;
            }
            if (i.GetType() != type)
            {
                await App.Current.MainPage.Navigation.PushModalAsync(page, isAnimated);
                return;
            }
        }
        public static async Task PopAsync()
        {
            try
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        public static async Task PopModelAsync()
        {
            try
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
