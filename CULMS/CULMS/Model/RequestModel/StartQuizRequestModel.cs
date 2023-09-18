using Newtonsoft.Json;

namespace CULMS.Model.RequestModel
{
    public class StartQuizRequestModel
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("courseQuizId")]
        public int CourseQuizId { get; set; }
    }
}
