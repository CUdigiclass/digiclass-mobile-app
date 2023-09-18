using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllCoursePage : ContentPage
    {
        int pagenumber = 1;
        public AllCoursePage()
        {
            InitializeComponent();
            vm.GetCourseMethod(1);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //vm.GetCourseMethod(1);
        }

        //private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        //{
        //    var scrollView = sender as ScrollView;
        //    double scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;
        //    if (scrollingSpace <= e.ScrollY)
        //    {
        //        //reached end
        //        pagenumber++;
        //        vm.GetCourseMethod(pagenumber);
        //    }
        //}
    }
}