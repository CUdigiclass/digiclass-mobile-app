using Newtonsoft.Json;

namespace CULMS.Model.RequestModel
{
    public class LoginRequestModel
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; } 
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }
        [JsonProperty("isMobileDevice")]
        public bool IsMobileDevice { get; set; }
    }
}
