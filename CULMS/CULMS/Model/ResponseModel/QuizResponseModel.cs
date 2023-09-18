using CULMS.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CULMS.Model.ResponseModel
{
    public class QuizResponseModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public QuizData Data { get; set; }
    }
    public class QuizData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("quizName")]
        public string QuizName { get; set; }

        [JsonProperty("topicName")]
        public string TopicName { get; set; }

        [JsonProperty("courseId")]
        public int CourseId { get; set; }

        [JsonProperty("assignmentType")]
        public string AssignmentType { get; set; }

        [JsonProperty("timeDuration")]
        public int TimeDuration { get; set; }

        [JsonProperty("quizStartDate")]
        public DateTime QuizStartDate { get; set; }

        [JsonProperty("quizEndDate")]
        public DateTime QuizEndDate { get; set; }

        [JsonProperty("attempt")]
        public int Attempt { get; set; }

        [JsonProperty("questions")]
        public List<QuestionData> Questions { get; set; }
    }
    public class QuestionData : ObservableObject
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("questionName")]
        public string QuestionName { get; set; }
        public int QuestionNumber { get; set; }

        [JsonProperty("questionImage")]
        public string QuestionImage { get; set; }

        [JsonProperty("questionImageUrl")]
        public string QuestionImageUrl { get; set; }

        [JsonProperty("option1")]
        public string Option1 { get; set; }

        [JsonProperty("option2")]
        public string Option2 { get; set; }

        [JsonProperty("option3")]
        public string Option3 { get; set; }

        [JsonProperty("option4")]
        public string Option4 { get; set; }

        [JsonProperty("courseQuizId")]
        public int CourseQuizId { get; set; }

        [JsonProperty("studentAnswer")]
        public object StudentAnswer { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("descriptions")]
        public string Descriptions { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

        [JsonProperty("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonProperty("lastModifiedOn")]
        public DateTime LastModifiedOn { get; set; }

        [JsonProperty("firstEnteredBy")]
        public string FirstEnteredBy { get; set; }

        [JsonProperty("firstEnteredOn")]
        public DateTime FirstEnteredOn { get; set; }

        [JsonProperty("pageNo")]
        public int PageNo { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
        public ObservableCollection<testModel> TestList { get; set; }
    }
    public class testModel : ObservableObject
    {
        private Color answerBackgroundColor = Color.WhiteSmoke;
        private Color answerTextColor = Color.Black;


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
        public string OptionName { get; set; }
       
        public string AnswerName { get; set; }
        public int Id { get; set; }
    }
}
