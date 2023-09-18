using Newtonsoft.Json;
using System;

namespace CULMS.Model.ResponseModel
{
    public class CourseDetailResponseModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public CourseDetail Data { get; set; }
    }
    public class CourseDetail
    {
        [JsonProperty("courseId")]
        public int CourseId { get; set; }

        [JsonProperty("courseName")]
        public string CourseName { get; set; }
        [JsonProperty("bannerImageName")]
        public string BannerImageName { get; set; }
        [JsonProperty("descriptions")]
        public string Descriptions { get; set; }
        [JsonProperty("courseOutCome")]
        public string CourseOutCome { get; set; }

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

        [JsonProperty("disciplineId")]
        public int DisciplineId { get; set; }

        [JsonProperty("subjectId")]
        public int SubjectId { get; set; }

        [JsonProperty("semester")]
        public int Semester { get; set; }

        [JsonProperty("bannerName")]
        public string BannerName { get; set; }

        [JsonProperty("courseSession")]
        public int CourseSession { get; set; }

        [JsonProperty("programType")]
        public int ProgramType { get; set; }

        [JsonProperty("courseCredit")]
        public int CourseCredit { get; set; }

        [JsonProperty("courseStatus")]
        public string CourseStatus { get; set; }

        [JsonProperty("isDraft")]
        public bool IsDraft { get; set; }

        [JsonProperty("courseDoc")]
        public string CourseDoc { get; set; }

        [JsonProperty("courseVideo")]
        public string CourseVideo { get; set; }

        [JsonProperty("isEnrolled")]
        public bool IsEnrolled { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
