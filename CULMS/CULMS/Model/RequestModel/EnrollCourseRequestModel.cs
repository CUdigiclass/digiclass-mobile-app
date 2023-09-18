using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CULMS.Model.RequestModel
{
    public class EnrollCourseRequestModel
    {
        [JsonProperty("learnerUserId")]
        public string LearnerUserId { get; set; }

        [JsonProperty("courseId")]
        public int CourseId { get; set; }
    }
}
