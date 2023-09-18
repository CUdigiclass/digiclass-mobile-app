using Newtonsoft.Json;

namespace CULMS.Model.RequestModel
{
    public class GetEnrolledCourseRequestModel
    {

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
