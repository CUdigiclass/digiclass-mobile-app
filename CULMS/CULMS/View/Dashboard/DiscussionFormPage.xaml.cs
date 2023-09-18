using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiscussionFormPage : ContentPage
    {
        int pagenumber = 1;
        int courseid = 0;
        public DiscussionFormPage(int courseId = 0)
        {
            InitializeComponent();
            courseid = courseId;
            vm.DiscussionQuestionMethod(courseId,1);
        }
        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            var scrollView = sender as ScrollView;
            double scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;
            if (scrollingSpace <= e.ScrollY)
            {
                //reached end
                pagenumber++;
                vm.DiscussionQuestionMethod(courseid, pagenumber);
            }
        }
    }
}