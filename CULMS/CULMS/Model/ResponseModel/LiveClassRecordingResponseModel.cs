using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CULMS.Model.ResponseModel
{
    public class LiveClassRecordingResponseModel
    {
        [JsonProperty("statusCode")]
        public int statusCode { get; set; }

        [JsonProperty("recordCount")]
        public int recordCount { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("data")]
        public List<LiveClassRecordingData> data { get; set; }
    }
    public class LiveClassRecordingData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("timeDuration")]
        public int TimeDuration { get; set; }

        [JsonProperty("liveDate")]
        public DateTime LiveDate { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("endTime")]
        public string EndTime { get; set; }

        [JsonProperty("courseId")]
        public int CourseId { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileUrl")]
        public string FileUrl { get; set; }

        [JsonProperty("courseName")]
        public string CourseName { get; set; }

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
