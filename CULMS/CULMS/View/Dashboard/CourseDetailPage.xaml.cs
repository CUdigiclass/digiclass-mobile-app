using CULMS.Model.ResponseModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailPage : ContentPage
    {
       // public CourseDetailPage(AllCourseData data = null)
        public CourseDetailPage()
        {
            InitializeComponent();
            //vm.CourseDetailMethod(data);
        }
    }
}