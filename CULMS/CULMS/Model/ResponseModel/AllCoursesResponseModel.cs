using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CULMS.Model.ResponseModel
{
    public class AllCoursesResponseModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public List<AllCourseData> Data { get; set; }
    }
    public class AllCourseData
    {
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

        [JsonProperty("guestsPermitted")]
        public bool GuestsPermitted { get; set; }

        [JsonProperty("bannerImageName")]
        public string BannerImageName { get; set; }

        [JsonProperty("contentViewConfigurationId")]
        public int ContentViewConfigurationId { get; set; }

        [JsonProperty("courseViewConfigurationId")]
        public int CourseViewConfigurationId { get; set; }

        [JsonProperty("autherName")]
        public string AutherName { get; set; }

        [JsonProperty("disciplineId")]
        public int DisciplineId { get; set; }

        [JsonProperty("subjectId")]
        public int SubjectId { get; set; }

        [JsonProperty("semester")]
        public int Semester { get; set; }

        [JsonProperty("isEnrolled")]
        public bool IsEnrolled { get; set; }

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
    }
}
