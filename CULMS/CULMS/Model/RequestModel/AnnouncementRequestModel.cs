using Newtonsoft.Json;

namespace CULMS.Model.RequestModel
{
    public class AnnouncementRequestModel
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("roleId")]
        public int RoleId { get; set; }
    }
}
