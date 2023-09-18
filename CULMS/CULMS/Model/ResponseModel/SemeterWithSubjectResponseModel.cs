using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CULMS.Model.ResponseModel
{
    public class SemeterWithSubjectResponseModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public SemeterWithSubjectData Data { get; set; }
    }

    public class SemeterWithSubjectData
    {
        [JsonProperty("courseSemesterSubject")]
        public List<CourseSemesterSubject> CourseSemesterSubject { get; set; }

        [JsonProperty("courseSemester")]
        public List<CourseSemester> CourseSemester { get; set; }
    }
    public class CourseSemester
    {
        [JsonProperty("semesterId")]
        public int SemesterId { get; set; }

        [JsonProperty("semesterName")]
        public string SemesterName { get; set; }
    }

    public class CourseSemesterSubject
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("courseId")]
        public int CourseId { get; set; }

        [JsonProperty("semester")]
        public int Semester { get; set; }

        [JsonProperty("streamId")]
        public int StreamId { get; set; }

        [JsonProperty("subjects")]
        public string Subjects { get; set; }

        [JsonProperty("courseName")]
        public string CourseName { get; set; }

        [JsonProperty("subjectName")]
        public string SubjectName { get; set; }

        [JsonProperty("semesterName")]
        public string SemesterName { get; set; }

        [JsonProperty("streamName")]
        public string StreamName { get; set; }

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
