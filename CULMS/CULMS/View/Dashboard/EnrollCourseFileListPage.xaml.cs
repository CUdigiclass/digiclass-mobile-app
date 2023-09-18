using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnrollCourseFileListPage : ContentPage
    {
        public EnrollCourseFileListPage(int CourseId,string bannerImage)
        {
            InitializeComponent();
            vm.GetEnrollCourseFileMethod(CourseId, bannerImage);
        }
        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
           
        //    //vm.GetCourseMethod();
        //}
    }
}