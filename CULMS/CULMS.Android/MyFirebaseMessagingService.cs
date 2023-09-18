using Android.App;
using Android.Content;
using Android.Util;
using AndroidX.Core.App;
using Firebase.Messaging;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;

namespace CULMS.Droid
{
    [Service(Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            var body = message.GetNotification().Body;
            Log.Debug(TAG, "Notification Message Body: " + body);
            SendNotification(body, message.Data);
        }
        void SendNotification(string messageBody, IDictionary<string, string> data)
        {
            try
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                foreach (var key in data.Keys)
                {
                    intent.PutExtra(key, data[key]);
                }
                PendingIntent pendingIntent = null;
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.S)
                {
                    pendingIntent = PendingIntent.GetActivity
                           (this,
                                                              MainActivity.NOTIFICATION_ID,
                                                              intent,
                                                              PendingIntentFlags.Mutable);
                }
                else
                {
                    pendingIntent = PendingIntent.GetActivity
                           (this,
                                                              MainActivity.NOTIFICATION_ID,
                                                              intent,
                                                              PendingIntentFlags.OneShot);
                }
                var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                      .SetSmallIcon(Resource.Drawable.icon_notification_bell)
                                      .SetContentTitle("FCM Message")
                                      .SetContentText(messageBody)
                                      .SetAutoCancel(true)
                                      .SetShowWhen(false)
                                      .SetPriority((int)NotificationImportance.Max)
                                      .SetColor(1111)
                                      .SetFullScreenIntent(pendingIntent, true)
                                      .SetVisibility((int)NotificationVisibility.Public)
                                      .SetContentIntent(pendingIntent);
                var notificationManager = NotificationManagerCompat.From(this);
                notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}