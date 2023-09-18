using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CULMS.Model
{
    public class AuthResponseObject<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int? ExpiresIn { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
