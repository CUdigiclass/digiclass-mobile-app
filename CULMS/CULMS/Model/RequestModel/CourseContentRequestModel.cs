using Newtonsoft.Json;

namespace CULMS.Model.RequestModel
{
    public class CourseContentRequestModel
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("roleId")]
        public int RoleId { get; set; }

        [JsonProperty("courseId")]
        public int CourseId { get; set; }

        [JsonProperty("isWeek")]
        public bool IsWeek { get; set; }

        [JsonProperty("courseModuleId")]
        public int CourseModuleId { get; set; }

        [JsonProperty("contentType")]
        public int ContentType { get; set; }

        [JsonProperty("weekId")]
        public int WeekId { get; set; }

        [JsonProperty("subjectId")]
        public int SubjectId { get; set; }
    }
}
