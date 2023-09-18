using Newtonsoft.Json;

namespace CULMS.Model.ResponseModel
{
    public class EnrollCourseResponseModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public bool Data { get; set; }
    }
}
