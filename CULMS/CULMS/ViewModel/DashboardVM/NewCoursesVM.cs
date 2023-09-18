using CULMS.Helpers;
using CULMS.Model;
using CULMS.View.Dashboard;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CULMS.ViewModel.DashboardVM
{
    public class NewCoursesVM : ObservableObject
    {
        #region Private Properties

        private ObservableCollection<CommonModel> newCoursesList;
        #endregion

        #region Public Properties


        public ObservableCollection<CommonModel> NewCoursesList
        {
            get { return newCoursesList; }
            set { newCoursesList = value; OnPropertyChanged(nameof(NewCoursesList)); }
        }

        #endregion

        #region Methods

        public NewCoursesVM()
        {
            NewCoursesList = GetNewCoursesList();
        }

        private ObservableCollection<CommonModel> GetNewCoursesList()
        {
            return new ObservableCollection<CommonModel>()
            {
                new CommonModel {CourseImage ="Animation", CourseName="Animation"},
                new CommonModel {CourseImage ="B_Tech", CourseName="B.Tech"},
                new CommonModel {CourseImage ="commerce", CourseName="Commerce"},
                new CommonModel {CourseImage ="econtent", CourseName="Pdf available", PDFURL ="https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf"},
                new CommonModel {CourseImage ="mba", CourseName="MBA"},
                new CommonModel {CourseImage ="Animation", CourseName="Animation"},
                new CommonModel {CourseImage ="econtent", CourseName="Pdf available", PDFURL="https://www.orimi.com/pdf-test.pdf"},
                new CommonModel {CourseImage ="B_Tech", CourseName="B.Tech"},
                new CommonModel {CourseImage ="commerce", CourseName="Commerce"},
                new CommonModel {CourseImage ="mba", CourseName="MBA"},
            };
        }
        #endregion

        #region Commands

        [System.Obsolete]
        public Command VideoCommand => new Command(async (param) =>
        {
            try
            {
                var data = param as CommonModel;
                if (data.PDFURL != null)
                {
                    //await Application.Current.MainPage.Navigation.PushModalAsync(new NewPDFView());
                    //Device.OpenUri(new Uri(data.PDFURL));
                    await Application.Current.MainPage.DisplayAlert("Alert", "No Pdf available.", "Ok");
                }
                else
                {
                    // await Application.Current.MainPage.Navigation.PushModalAsync(new MediaManager.Forms.VideoPage());
                    await Application.Current.MainPage.Navigation.PushModalAsync(new VideoPage("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4"));
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        });

        #endregion
    }
}
