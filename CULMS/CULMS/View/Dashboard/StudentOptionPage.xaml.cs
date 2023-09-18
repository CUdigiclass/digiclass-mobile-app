using CULMS.Model.ResponseModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentOptionPage : ContentPage
    {
        public StudentOptionPage(Configuration data = null)
        {
            InitializeComponent();
            vm.ConfigurationId = data.ConfigurationId;
        }
    }
}