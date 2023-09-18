using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CULMS.Model.RequestModel
{
    public class EnrollCourseFileRequestModel
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("courseId")]
        public int CourseId { get; set; }
    }
}
