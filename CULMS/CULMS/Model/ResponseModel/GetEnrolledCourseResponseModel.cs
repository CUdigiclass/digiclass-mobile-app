using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CULMS.Model.ResponseModel
{
    public class GetEnrolledCourseResponseModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("recordCount")]
        public int SecordCount { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public List<EnrolledCourseDatum> data { get; set; }
    }
    public class EnrolledCourseDatum
    {
        [JsonProperty("learnerUserId")]
        public string LearnerUserId { get; set; }

        [JsonProperty("isEnrolled")]
        public bool IsEnrolled { get; set; }

        [JsonProperty("courseId")]
        public int CourseId { get; set; }

        [JsonProperty("courseName")]
        public string CourseName { get; set; }

        [JsonProperty("courseCode")]
        public string CourseCode { get; set; }

        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("availble")]
        public bool Availble { get; set; }

        [JsonProperty("durationConfigurationId")]
        public int DurationConfigurationId { get; set; }

        [JsonProperty("enrolledCount")]
        public int EnrolledCount { get; set; }

        [JsonProperty("dStartDate")]
        public DateTime DStartDate { get; set; }

        [JsonProperty("dEndDate")]
        public DateTime DEndDate { get; set; }

        [JsonProperty("dStartTime")]
        public string DStartTime { get; set; }

        [JsonProperty("dEndTime")]
        public string DEndTime { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDelete")]
        public bool IsDelete { get; set; }

        [JsonProperty("bannerImageName")]
        public string BannerImageName { get; set; }

        [JsonProperty("contentViewConfigurationId")]
        public int ContentViewConfigurationId { get; set; }

        [JsonProperty("courseViewConfigurationId")]
        public int CourseViewConfigurationId { get; set; }

        [JsonProperty("autherName")]
        public string AutherName { get; set; }

        [JsonProperty("semester")]
        public int Semester { get; set; }
    }
}
