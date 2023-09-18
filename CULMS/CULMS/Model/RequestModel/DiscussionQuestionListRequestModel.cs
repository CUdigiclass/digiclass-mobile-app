using Newtonsoft.Json;

namespace CULMS.Model.RequestModel
{
    public class DiscussionQuestionListRequestModel
    {
        [JsonProperty("courseId")]
        public string CourseId { get; set; }

        [JsonProperty("pageNo")]
        public int PageNo { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("roleId")]
        public int RoleId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
