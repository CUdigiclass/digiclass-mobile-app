using CULMS.Helpers;
using CULMS.View.Dashboard;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CULMS.ViewModel.PartialViewModel
{
    public class TabbedViewVM : ObservableObject
    {
        #region Private Properties

        private int homeImageHeight;
        private int announcementImageHeight;
        private int profileImageHeight;
        private string homeTabSelected = "SelectedHome";
        private string announcementTabSelected = "UnSelectedAnnouncements";
        private string profileTabSelected = "UnselectedProfile";
        private string logoutTabSelected = "UnselectedLogout";
        private LayoutOptions homeTabbedVerticalOptions;
        private LayoutOptions announcementTabbedVerticalOptions;
        private LayoutOptions profileTabbedVerticalOptions;
        private LayoutOptions logoutTabbedVerticalOptions;
        private int logoutImageHeight;
        #endregion

        #region Public Properties

        public LayoutOptions LogoutTabbedVerticalOptions
        {
            get { return logoutTabbedVerticalOptions; }
            set { logoutTabbedVerticalOptions = value; OnPropertyChanged(nameof(LogoutTabbedVerticalOptions)); }
        }

        public string LogoutTabSelected
        {
            get { return logoutTabSelected; }
            set { logoutTabSelected = value; OnPropertyChanged(nameof(LogoutTabSelected)); }
        }

        public int LogoutImageHeight
        {
            get { return logoutImageHeight; }
            set { logoutImageHeight = value; OnPropertyChanged(nameof(LogoutImageHeight)); }
        }

        public LayoutOptions ProfileTabbedVerticalOptions
        {
            get { return profileTabbedVerticalOptions; }
            set { profileTabbedVerticalOptions = value; OnPropertyChanged(nameof(ProfileTabbedVerticalOptions)); }
        }

        public string ProfileTabSelected
        {
            get { return profileTabSelected; }
            set { profileTabSelected = value; OnPropertyChanged(nameof(ProfileTabSelected)); }
        }

        public int ProfileImageHeight
        {
            get { return profileImageHeight; }
            set { profileImageHeight = value; OnPropertyChanged(nameof(ProfileImageHeight)); }
        }

        public LayoutOptions AnnouncementTabbedVerticalOptions
        {
            get { return announcementTabbedVerticalOptions; }
            set { announcementTabbedVerticalOptions = value; OnPropertyChanged(nameof(AnnouncementTabbedVerticalOptions)); }
        }

        public int AnnouncementImageHeight
        {
            get { return announcementImageHeight; }
            set { announcementImageHeight = value; OnPropertyChanged(nameof(AnnouncementImageHeight)); }
        }

        public string AnnouncementTabSelected
        {
            get { return announcementTabSelected; }
            set { announcementTabSelected = value; OnPropertyChanged(nameof(AnnouncementTabSelected)); }
        }

        public LayoutOptions HomeTabbedVerticalOptions
        {
            get { return homeTabbedVerticalOptions; }
            set { homeTabbedVerticalOptions = value; OnPropertyChanged(nameof(HomeTabbedVerticalOptions)); }
        }

        public string HomeTabSelected
        {
            get { return homeTabSelected; }
            set { homeTabSelected = value; OnPropertyChanged(nameof(HomeTabSelected)); }
        }

        public int HomeImageHeight
        {
            get { return homeImageHeight; }
            set { homeImageHeight = value; OnPropertyChanged(nameof(HomeImageHeight)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Constructor of TabbedView 
        /// </summary>
        public TabbedViewVM()
        {
            int tabId = Preferences.Get(StringConstant.TabIdKey, 0);
            switch (tabId)
            {
                case 0:
                case 1:
                    HomeTabSelected = "SelectedHome.png";
                    AnnouncementTabSelected = "UnselectedLeave.png";
                    ProfileTabSelected = "UnselectedProfile.png";
                    LogoutTabSelected = "UnselectedMessage.png";
                    HomeTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    AnnouncementTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    ProfileTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    LogoutTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    HomeImageHeight = 50;
                    AnnouncementImageHeight = 50;
                    ProfileImageHeight = 50;
                    LogoutImageHeight = 50;
                    break;
                case 2:
                    HomeTabSelected = "UnselectedHome.png";
                    AnnouncementTabSelected = "SelectedLeave.png";
                    ProfileTabSelected = "UnselectedProfile.png";
                    LogoutTabSelected = "UnselectedMessage.png";
                    HomeTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    AnnouncementTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    ProfileTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    LogoutTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    HomeImageHeight = 50;
                    AnnouncementImageHeight = 50;
                    ProfileImageHeight = 50;
                    LogoutImageHeight = 50;
                    break;
                case 3:
                    HomeTabSelected = "UnselectedHome.png";
                    AnnouncementTabSelected = "UnselectedLeave.png";
                    ProfileTabSelected = "SelectedProfile.png";
                    LogoutTabSelected = "UnselectedMessage.png";
                    HomeTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    AnnouncementTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    ProfileTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    LogoutTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    HomeImageHeight = 50;
                    AnnouncementImageHeight = 50;
                    ProfileImageHeight = 50;
                    LogoutImageHeight = 50;
                    break;
                case 4:
                    HomeTabSelected = "UnselectedHome.png";
                    AnnouncementTabSelected = "UnselectedLeave.png";
                    ProfileTabSelected = "UnselectedProfile.png";
                    LogoutTabSelected = "SelectedMessage.png";
                    HomeTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    AnnouncementTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    ProfileTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    LogoutTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    HomeImageHeight = 50;
                    AnnouncementImageHeight = 50;
                    ProfileImageHeight = 50;
                    LogoutImageHeight = 50;
                    break;
                default:
                    HomeTabSelected = "SelectedHome.png";
                    AnnouncementTabSelected = "UnselectedLeave.png";
                    ProfileTabSelected = "UnselectedProfile.png";
                    LogoutTabSelected = "UnselectedMessage.png";
                    HomeTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    AnnouncementTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    ProfileTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    LogoutTabbedVerticalOptions = LayoutOptions.CenterAndExpand;
                    HomeImageHeight = 50;
                    AnnouncementImageHeight = 50;
                    ProfileImageHeight = 50;
                    LogoutImageHeight = 50;
                    LogoutImageHeight = 50;
                    break;
            }
        }
        #endregion

        #region Commands

        /// <summary>
        /// Home Page Command
        /// </summary>
        private Command homeTabCommand;
        public Command HomeTabCommand
        {
            get
            {
                try
                {
                    if (homeTabCommand == null)
                        homeTabCommand = new Command(() =>
                        {
                            Preferences.Set(StringConstant.TabIdKey, 1);
                            Application.Current.MainPage = new NavigationPage(new HomePage());
                        });
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return homeTabCommand;
            }
        }

        /// <summary>
        /// Command to navigate Leave page
        /// </summary>
        private Command announcementTabCommand;
        public Command AnnouncementTabCommand
        {
            get
            {
                try
                {
                    if (announcementTabCommand == null)
                        announcementTabCommand = new Command(() =>
                        {
                            Preferences.Set(StringConstant.TabIdKey, 2);
                            Application.Current.MainPage = new NavigationPage(new NewsPage());
                        });
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return announcementTabCommand;
            }
        }

        /// <summary>
        /// Command to navigate Profile page
        /// </summary>
        private Command profileTabCommand;
        public Command ProfileTabCommand
        {
            get
            {
                try
                {
                    if (profileTabCommand == null)
                        profileTabCommand = new Command(() =>
                        {
                            Preferences.Set(StringConstant.TabIdKey, 3);
                            Application.Current.MainPage = new NavigationPage(new ProfilePage());
                        });
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return profileTabCommand;
            }
        }

        /// <summary>
        /// Command to navigate Message Page
        /// </summary>
        private Command messageTabCommand;
        public Command MessageTabCommand
        {
            get
            {
                try
                {
                    if (messageTabCommand == null)
                        messageTabCommand = new Command(() =>
                        {
                            Preferences.Set(StringConstant.TabIdKey, 4);
                            Application.Current.MainPage = new NavigationPage(new MessagePage());
                        });
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return messageTabCommand;
            }
        }
        #endregion
    }
}
