using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.OS;
using CULMS.Droid.Helper;
using CULMS.Model;
using System;
using Xamarin.Forms;
using Android.Content;
using Android.Gms.Auth.Api;
using Microsoft.AppCenter.Crashes;

[assembly: Dependency(typeof(GoogleManager))]
namespace CULMS.Droid.Helper
{
    [Obsolete]
    internal class GoogleManager : Java.Lang.Object, IGoogleManager, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        public Action<GoogleUser, string> _onLoginComplete;

        [Obsolete]
        public static GoogleApiClient _googleApiClient { get; set; }
        public static GoogleManager Instance { get; private set; }
        Context _context;

        public GoogleManager()
        {
            _context = global::Android.App.Application.Context;
            Instance = this;
        }

        public void Login(Action<GoogleUser, string> onLoginComplete)
        {
            try
            {
                GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                                                                   .RequestEmail()
                                                                   .Build();
                _googleApiClient = new GoogleApiClient.Builder((_context).ApplicationContext)
                    .AddConnectionCallbacks(this)
                    .AddOnConnectionFailedListener(this)
                    .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                    .AddScope(new Scope(Scopes.Profile))
                    .Build();
                _onLoginComplete = onLoginComplete;
                Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_googleApiClient);
                ((MainActivity)Forms.Context).StartActivityForResult(signInIntent, 1);
                _googleApiClient.Connect();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        public void Logout()
        {
            try
            {
                var gsoBuilder = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn).RequestEmail();

                GoogleSignIn.GetClient(_context, gsoBuilder.Build())?.SignOut();

                _googleApiClient.Disconnect();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }

        public void OnAuthCompleted(GoogleSignInResult result)
        {
            try
            {
                if (result.IsSuccess)
                {
                    GoogleSignInAccount accountt = result.SignInAccount;
                    _onLoginComplete?.Invoke(new GoogleUser()
                    {
                        Name = accountt.DisplayName,
                        Email = accountt.Email,
                        Picture = new Uri((accountt.PhotoUrl != null ? $"{accountt.PhotoUrl}" : $"https://autisticdating.net/imgs/profile-placeholder.jpg"))
                    }, string.Empty);
                }
                else
                {
                    _onLoginComplete?.Invoke(null, "An error occured!");
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        public void OnConnected(Bundle connectionHint)
        {

        }

        public void OnConnectionSuspended(int cause)
        {
            _onLoginComplete?.Invoke(null, "Canceled!");
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            _onLoginComplete?.Invoke(null, result.ErrorMessage);
        }
    }
}