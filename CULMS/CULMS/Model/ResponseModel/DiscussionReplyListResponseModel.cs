using Newtonsoft.Json;
using static Xamarin.Forms.Internals.Profile;
using System.Collections.Generic;
using System;

namespace CULMS.Model.ResponseModel
{
    public class DiscussionReplyListResponseModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public List<DiscussionReplyListData> Data { get; set; }
    }
    public class DiscussionReplyListData
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("discussionQuestionId")]
        public int DiscussionQuestionId { get; set; }

        [JsonProperty("courseId")]
        public int CourseId { get; set; }

        [JsonProperty("reply")]
        public string Reply { get; set; }

        [JsonProperty("discussionDoc")]
        public List<DiscussionDocList> DiscussionDoc { get; set; }

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
    public class DiscussionDocList
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("discussionQuestionId")]
        public int DiscussionQuestionId { get; set; }

        [JsonProperty("discussionReplyId")]
        public int DiscussionReplyId { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileURL")]
        public string FileURL { get; set; }

        [JsonProperty("fileType")]
        public string FileType { get; set; }

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
    }
}
