using Newtonsoft.Json;

namespace CULMS.Model.RequestModel
{
    public class SemeterWithSubjectRequestModel
    {
        [JsonProperty("courseId")]
        public int CourseId { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
