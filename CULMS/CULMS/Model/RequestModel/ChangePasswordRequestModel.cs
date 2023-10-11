using Newtonsoft.Json;

namespace CULMS.Model.RequestModel
{
    public class ChangePasswordRequestModel
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("oldPassword")]
        public string OldPassword { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
