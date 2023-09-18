using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Firebase.Iid;
using Firebase.Messaging;
using System;
using Xamarin.Essentials;

namespace CULMS.Droid
{
    [Service(Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    [Obsolete]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public async override void OnTokenRefresh()
        {
            var token = await FirebaseMessaging.Instance.GetToken().AsAsync<Java.Lang.String>();
            var d = token.ToString();
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Preferences.Set("FCMToken", d);
            //Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            //Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
            //SendRegistrationToServer(refreshedToken);
        }
        void SendRegistrationToServer(string token)
        {
            // Add custom implementation, as needed.
        }
    }
}