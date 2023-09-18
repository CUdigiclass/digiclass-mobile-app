using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CULMS.Model
{
    public class ResponseObject<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
