using CULMS.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(CULMS.Droid.Helper.DependencyImplementation))]
namespace CULMS.Droid.Helper
{
    public class DependencyImplementation : IDependencyDemo
    {
        public string GetThePlatformMessage()
        {
            var d = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            return d;
        }
    }
}