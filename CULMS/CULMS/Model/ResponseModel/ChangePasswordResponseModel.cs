using Newtonsoft.Json;
using System;

namespace CULMS.Model.ResponseModel
{
    public class ChangePasswordResponseModel
    {
        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public ChangePasswordData Data { get; set; }
    }
    public class ChangePasswordData
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("roleId")]
        public int RoleId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public object LastName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("phoneCode")]
        public string PhoneCode { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("empId")]
        public string EmpId { get; set; }

        [JsonProperty("semester")]
        public int Semester { get; set; }

        [JsonProperty("lastModifiedOn")]
        public DateTime LastModifiedOn { get; set; }
    }
}
