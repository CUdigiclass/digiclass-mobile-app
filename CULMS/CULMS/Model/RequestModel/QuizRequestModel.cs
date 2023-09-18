using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace CULMS.Model.RequestModel
{
    public class QuizRequestModel
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("descriptions")]
        public string Descriptions { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

        [JsonProperty("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonProperty("lastModifiedOn")]
        public DateTime LastModifiedOn { get; set; }

        [JsonProperty("firstEnteredBy")]
        public string FirstEnteredBy { get; set; }

        [JsonProperty("firstEnteredOn")]
        public DateTime FirstEnteredOn { get; set; }

        [JsonProperty("pageNo")]
        public int PageNo { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("question_name")]
        public string Question_name { get; set; }

        [JsonProperty("question_text")]
        public string Question_text { get; set; }

        [JsonProperty("option1")]
        public string Option1 { get; set; }

        [JsonProperty("option2")]
        public string Option2 { get; set; }

        [JsonProperty("option3")]
        public string Option3 { get; set; }

        [JsonProperty("option4")]
        public string Option4 { get; set; }

        [JsonProperty("correct_answer")]
        public string Correct_answer { get; set; }

        [JsonProperty("question_status")]
        public string Question_status { get; set; }

        [JsonProperty("question_exam_id")]
        public int Question_exam_id { get; set; }
      
    }
    
}
