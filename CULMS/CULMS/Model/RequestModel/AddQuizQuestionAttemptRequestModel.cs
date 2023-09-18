using Newtonsoft.Json;

namespace CULMS.Model.RequestModel
{
    public class AddQuizQuestionAttemptRequestModel
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("studentAnswer")]
        public string StudentAnswer { get; set; }

        [JsonProperty("quizQuestionId")]
        public int QuizQuestionId { get; set; }

        [JsonProperty("courseQuizId")]
        public int CourseQuizId { get; set; }
    }
}
