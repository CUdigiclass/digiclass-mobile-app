using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CULMS.Model.RequestModel
{
    public class QuizResultResponseModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public List<QuizResultData> Data { get; set; }
    }
    public class QuizResultData
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("studentName")]
        public string StudentName { get; set; }

        [JsonProperty("totalMark")]
        public double TotalMark { get; set; }

        [JsonProperty("obtainedMarks")]
        public double ObtainedMarks { get; set; }

        [JsonProperty("attendQuizDate")]
        public DateTime AttendQuizDate { get; set; }

        [JsonProperty("quizAttempt")]
        public int QuizAttempt { get; set; }

        [JsonProperty("attemptQuestion")]
        public int AttemptQuestion { get; set; }

        [JsonProperty("totalQuestion")]
        public int TotalQuestion { get; set; }

        [JsonProperty("totalCorrectQs")]
        public int TotalCorrectQs { get; set; }

        [JsonProperty("totalInCorrectQs")]
        public int TotalInCorrectQs { get; set; }

        [JsonProperty("courseQuizId")]
        public int CourseQuizId { get; set; }
    }
}
