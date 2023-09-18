using Newtonsoft.Json;

namespace CULMS.Model.ResponseModel
{
    public class LoginResponseModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public LoginData Data { get; set; }
    }
    public class LoginData
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expires_In")]
        public int Expires_In { get; set; }
    }
}
