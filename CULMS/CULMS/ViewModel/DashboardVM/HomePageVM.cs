using CULMS.Helpers;
using CULMS.Helpers.APIHelper;
using CULMS.Model;
using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using CULMS.Services;
using CULMS.View.Auth;
using CULMS.View.Dashboard;
using CULMS.View.PartialView;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CULMS.ViewModel.DashboardVM
{
    public class HomePageVM : ObservableObject
    {
       
        #region Private Properties

        private ObservableCollection<EnrolledCourseDatum> enrolledCourseList;
        private ObservableCollection<CommonModel> courcesList;
        private ObservableCollection<CommonModel> popularSkillsList;
        private ObservableCollection<CommonModel> pathList;
        #endregion

        #region Public Properties

        private ObservableCollection<CommonModel> hamburgerMenuItemList;
        private bool isVisibleLogoutFrame;
        private string userName;
        private string uID;

        public string UID
        {
            get { return uID; }
            set { uID = value; OnPropertyChanged(nameof(UID)); }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(nameof(UserName)); }
        }

        public bool IsVisibleLogoutFrame
        {
            get { return isVisibleLogoutFrame; }
            set { isVisibleLogoutFrame = value; OnPropertyChanged(nameof(IsVisibleLogoutFrame)); }
        }

        public ObservableCollection<EnrolledCourseDatum> EnrolledCourseList
        {
            get { return enrolledCourseList; }
            set { enrolledCourseList = value; OnPropertyChanged(nameof(EnrolledCourseList)); }
        }

        public ObservableCollection<CommonModel> HamburgerMenuItemList
        {
            get { return hamburgerMenuItemList; }
            set { hamburgerMenuItemList = value; OnPropertyChanged(nameof(HamburgerMenuItemList)); }
        }

        private bool hamburgerVisibility;

        public bool HamburgerVisibility
        {
            get { return hamburgerVisibility; }
            set { hamburgerVisibility = value; OnPropertyChanged(nameof(HamburgerVisibility)); }
        }

        public ObservableCollection<CommonModel> PathList
        {
            get { return pathList; }
            set { pathList = value; OnPropertyChanged(nameof(PathList)); }
        }

        public ObservableCollection<CommonModel> PopularSkillsList
        {
            get { return popularSkillsList; }
            set { popularSkillsList = value; OnPropertyChanged(nameof(PopularSkillsList)); }
        }


        public ObservableCollection<CommonModel> CourcesList
        {
            get { return courcesList; }
            set { courcesList = value; OnPropertyChanged(nameof(CourcesList)); }
        }

        #endregion

        #region Methods

        public HomePageVM()
        {
            //StartCommand = new Command(StartTimerCommand);          
            
            MessagingCenter.Subscribe<HamburgerMenu>(this, "Hide", async (sender) =>
            {
                HamburgerVisibility = false;
            });
            IsVisibleLogoutFrame = false;
            ProfileDetailMethod();
        }
        private string email;
        private string name;
        private ImageSource userImage;

        public ImageSource UserImage
        {
            get { return userImage; }
            set { userImage = value; OnPropertyChanged(nameof(UserImage)); }
        }

        GoogleUser GoogleUser = new GoogleUser();

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Email
        {
            get { return email; }
            set { userImage = value; OnPropertyChanged(nameof(Email)); }
        }

        public void UserData(GoogleUser googleUser)
        {
            try
            {
                GoogleUser = googleUser;
                Name = googleUser.Name;
                Email = googleUser.Email;
                UserImage = googleUser.Picture;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        private async void ProfileDetailMethod()
        {
            try
            {
                var response = await ValidateTokenApi();
                if (response != null && response.StatusCode == 200)
                {
                    UserName = response.Data.FirstName;
                    UID = response.Data.UserId;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        public async void GetCourseMethod()
        {
            try
            {
                IsLoading = true;
                await Task.Delay(50);
                GetEnrolledCourseRequestModel getEnrolledCourseRequest = new GetEnrolledCourseRequestModel
                {
                    UserId = Preferences.Get(StringConstant.UserId, string.Empty)
                };
                var response = await GetEnrolledCourseAPI(getEnrolledCourseRequest);
                if (response != null && response.StatusCode == 200)
                {
                    EnrolledCourseList = new ObservableCollection<EnrolledCourseDatum>(response.data.Select(data => new EnrolledCourseDatum()
                    {
                        AutherName = data.AutherName,
                        CourseName = data.CourseName,
                        BannerImageName = data.BannerImageName,
                        CourseId = data.CourseId
                    }).Take(4).ToList());
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally { IsLoading = false; }
        }

        #endregion

        #region Commands
        public Command ShowLogoutFrame => new Command(() =>
        {
            try
            {
                IsLoading = true;
                IsVisibleLogoutFrame = true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
            } 
        });
        public Command HideLogoutPopupCommand => new Command(() =>
        {
            try
            {
                IsLoading = true;
                IsVisibleLogoutFrame = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        });
        public Command CourseDetailPageCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                Utilities.IsComeFromEnrollCourse = false;
                await Task.Delay(50);
                var data = param as EnrolledCourseDatum;
                Utilities.courseId = data.CourseId;
                //await Application.Current.MainPage.Navigation.PushModalAsync(new CourseDetailPage());
                await RichNavigation.PushAsync(new CourseDetailPage(), typeof(CourseDetailPage));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
            }   
        });

        public Command EnrollCourseFileListPageCommand => new Command(async (param) =>
        {
            try
            {
                IsLoading = true;
                await Task.Delay(50);
                var data = param as EnrolledCourseDatum;
                // await Application.Current.MainPage.Navigation.PushModalAsync(new EnrollCourseFileListPage(data.CourseId, data.BannerImageName));
                //await Application.Current.MainPage.Navigation.PushModalAsync(new SemesterWithSubjectPage(data.CourseId));
                await RichNavigation.PushAsync(new SemesterWithSubjectPage(data.CourseId), typeof(SemesterWithSubjectPage));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        });

        public Command HamburgerOptionCommand => new Command(async (param) =>
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                var data = param as CommonModel;
                switch (data.Text)
                {
                    case "My Courses":
                        //await Application.Current.MainPage.Navigation.PushModalAsync(new EnrolledCoursePage());
                        await RichNavigation.PushAsync(new EnrolledCoursePage(), typeof(EnrolledCoursePage));
                        break;
                    case "Catalog":
                        //await Application.Current.MainPage.Navigation.PushModalAsync(new AllCoursePage());
                        await RichNavigation.PushAsync(new AllCoursePage(), typeof(AllCoursePage));
                        break;
                    case "Change Password":
                        //await Application.Current.MainPage.Navigation.PushModalAsync(new AllCoursePage());
                        await RichNavigation.PushAsync(new ResetPasswordPage(), typeof(ResetPasswordPage));
                        break;
                    //case "Exam":
                    //    await Application.Current.MainPage.Navigation.PushModalAsync(new QuizListPage());
                    //    break;
                    default:
                        await App.Current.MainPage.DisplayAlert("Alert", data.Text + " Page Coming Soon...", "Okay");
                        break;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        });
        public Command HamburgerClicked => new Command(async () =>
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                HamburgerVisibility = HamburgerVisibility == true ? false : true;
                MessagingCenter.Send(this, "Translate");
                HamburgerMenuItemList = GetHamburgerMenuItemList();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        });

        private ObservableCollection<CommonModel> GetHamburgerMenuItemList()
        {
            return new ObservableCollection<CommonModel>()
            {
                new CommonModel(){Text ="Catalog", CourseImage ="All_Courses"},
                new CommonModel(){Text ="My Courses", CourseImage ="Courses"},
                new CommonModel(){Text ="Change Password", CourseImage ="changePassword"},
               // new CommonModel(){Text ="Exam", CourseImage ="exam"},
                //new CommonModel(){Text ="Calendar", CourseImage = "calendar"},
            };
        }

        public Command EnrolledCourseCommand => new Command(async () =>
        {
            try
            {
                IsLoading = true;
                await Task.Delay(50);
                //await Application.Current.MainPage.Navigation.PushModalAsync(new EnrolledCoursePage());
                await RichNavigation.PushAsync(new EnrolledCoursePage(), typeof(EnrolledCoursePage));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        });


        public Command NewCourseCommand => new Command(async() =>
        {
            try
            {
                IsLoading = true;
                //Application.Current.MainPage.Navigation.PushModalAsync(new AllCoursePage());
                await RichNavigation.PushAsync(new AllCoursePage(), typeof(AllCoursePage));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally { IsLoading = false; }
        });
        public Command LogoutCommand => new Command(async () =>
        {
            IsLoading = true;
            await Task.Delay(50);
            Preferences.Set(StringConstant.IsLogin, false);
            Preferences.Set("Token", null);
            Preferences.Set(StringConstant.UserId, string.Empty);
            Preferences.Set(StringConstant.RoleId, 0); 
            Preferences.Set("FCMToken", string.Empty);
            IsVisibleLogoutFrame = false;
            Application.Current.MainPage =new NavigationPage(new LoginPage());
            IsLoading = false;
        });

        public Command AllCourseCommand => new Command(async () =>
        {
            try
            {
                IsLoading = true;
                // await Application.Current.MainPage.Navigation.PushModalAsync(new CoursePage());
                await RichNavigation.PushAsync(new CoursePage(), typeof(CoursePage));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        });
        #endregion

        #region API Methods

        public async Task<GetEnrolledCourseResponseModel> GetEnrolledCourseAPI(GetEnrolledCourseRequestModel getEnrolledCourseRequest)
        {
            GetEnrolledCourseResponseModel loginResponse = new GetEnrolledCourseResponseModel();
            try
            {
                APICall aPICall = new APICall();
                string jsonRequest = JsonConvert.SerializeObject(getEnrolledCourseRequest);
                var courseUrl = StringConstant.ApiKeyUrl + StringConstant.ApeKeyGetEnrolledCourse;
                var apiResponse = await aPICall.PostRequestToken(courseUrl, jsonRequest, Preferences.Get("Token", null)).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        loginResponse = JsonConvert.DeserializeObject<GetEnrolledCourseResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return loginResponse;
        }
        public async Task<ValidateTokenResponseModel> ValidateTokenApi()
        {
            ValidateTokenResponseModel validateresponse = new ValidateTokenResponseModel();
            try
            {
                APICall aPICall = new APICall();
                var loginUrl = StringConstant.ApiKeyUrl + StringConstant.ApeKeyValidateToken + Preferences.Get("Token", ""); ;
                var apiResponse = await aPICall.GetRequest(loginUrl, string.Empty).ConfigureAwait(true);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiReponseData = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(apiReponseData))
                    {
                        validateresponse = JsonConvert.DeserializeObject<ValidateTokenResponseModel>(apiReponseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return validateresponse;
        }
        #endregion    
    }
}
