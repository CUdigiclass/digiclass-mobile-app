using CULMS.Helpers;
using Xamarin.Forms;

namespace CULMS.Model
{
    public class CommonModel : ObservableObject
    {
        public string Skills { get; set; }
        public string CourseImage { get; set; }
        public string CourseName { get; set; }
        public string PDFURL { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public string Centre1 { get; set; }
        public string Centre2 { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public string Option { get; set; }
        private Color answerBackgroundColor;
        private Color answerTextColor;

        public Color AnswerTextColor
        {
            get { return answerTextColor; }
            set { answerTextColor = value; OnPropertyChanged(nameof(AnswerTextColor)); }
        }

        public Color AnswerBackgroundColor
        {
            get { return answerBackgroundColor; }
            set { answerBackgroundColor = value; OnPropertyChanged(nameof(AnswerBackgroundColor)); }
        }
    }
}
