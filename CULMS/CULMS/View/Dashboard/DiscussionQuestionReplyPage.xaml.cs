using CULMS.Model.ResponseModel;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiscussionQuestionReplyPage : ContentPage
    {
        public DiscussionQuestionReplyPage(DiscussionQuestionListData data = null)
        {
            try
            {
                InitializeComponent();
                vm.DiscussionQuestionData(data);
                vm.GetReplyListMethod();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}