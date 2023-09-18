using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CULMS.Model.ResponseModel
{
    public class AnnouncementResponseModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public AnnouncementData Data { get; set; }
    }
    public class AnnouncementData
    {
        [JsonProperty("global")]
        public List<Global> Global { get; set; }

        [JsonProperty("stream")]
        public List<Stream> Stream { get; set; }

        [JsonProperty("course")]
        public List<Course> Course { get; set; }
    }
    public class Global
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("announcementId")]
        public int AnnouncementId { get; set; }

        [JsonProperty("announcements")]
        public string Announcements { get; set; }
        public string AnnouncementType { get; set; }
        public string PDFIcon { get; set; }

        [JsonProperty("visibleRole")]
        public string VisibleRole { get; set; }

        [JsonProperty("accessId")]
        public string AccessId { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileURL")]
        public string FileURL { get; set; }

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
    public class Stream
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("announcementId")]
        public int AnnouncementId { get; set; }

        [JsonProperty("announcements")]
        public string Announcements { get; set; }

        [JsonProperty("visibleRole")]
        public string VisibleRole { get; set; }

        [JsonProperty("accessId")]
        public string AccessId { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileURL")]
        public string FileURL { get; set; }
        public string AnnouncementType { get; set; }
        public string PDFIcon { get; set; }

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
    public class Course
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("announcementId")]
        public int AnnouncementId { get; set; }

        [JsonProperty("announcements")]
        public string Announcements { get; set; }

        [JsonProperty("visibleRole")]
        public string VisibleRole { get; set; }

        [JsonProperty("accessId")]
        public string AccessId { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileURL")]
        public string FileURL { get; set; }
        public string AnnouncementType { get; set; }
        public string PDFIcon { get; set; }

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
