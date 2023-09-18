using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CULMS.Model.ResponseModel
{
    public class QuizListResponseModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public QuizListData Data { get; set; }
    }
    public class QuizListData
    {
        [JsonProperty("gradedAssessment")]
        public List<GradedAssessment> GradedAssessment { get; set; }

        [JsonProperty("unGradedAssessment")]
        public List<UnGradedAssessment> UnGradedAssessment { get; set; }
    }
    public class UnGradedAssessment
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
    }
    public class GradedAssessment
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
    }
}
