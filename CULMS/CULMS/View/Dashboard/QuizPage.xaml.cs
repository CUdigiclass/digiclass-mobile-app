using CULMS.Model.ResponseModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizPage : ContentPage
    {
        public QuizPage(int Id = 0, int timeDuration = 0)
        {
            InitializeComponent();
            vm.QuizAPIMethod(Id, timeDuration);
        }
    }

}