namespace CULMS.Helpers
{
    public class StringConstant
    {
        //Production
        //public const string ApiKeyUrl = "http://43.240.66.78:7263/api/";
        ///Development
         //public const string ApiKeyUrl = "http://43.240.66.78:7265/api/";
         public const string ApiKeyUrl = "http://3.109.95.156:7265/api/";
        // public const string ApiKeyUrl = "http://172.17.19.82:8081/api/";
       // Student/GetByCourseDetailsId?courseId=84&userId=student123%40cumail.in


        public const string ApeKeyLogin = "Login/UserLogin";
        public const string ApeKeyValidateToken = "Login/UserValidate?token=";
        public const string ApeKeyGetEnrolledCourse= "Course/GetCourseListWithEnrollUser";
        public const string ApeKeyGetEnrollCourseFileList = "Course/GetEnrollCourseFileList";
        public const string ApeKeyGetEnrollCourseListWithoutEnrollUser = "Course/GetCourseListWithoutEnrollUser";
        public const string ApiKeyEnrollCourse = "EnrollCourse/AddEnrollCourse";
        public const string ApiKeyQuiz = "DigiclassQuiz/GetMCQById?quizId=";
        public const string ApiKeyCourseDetail= "Student/GetByCourseDetailsId?courseId=";
        public const string ApiKeySemesterWithSubject= "Student/GetSemesterWithSubject";
        public const string ApiKeyNews= "News/GetNewsList";
        public const string ApiKeyAnnouncement= "Announcement/GetAnnouncementListWithFilter";
        public const string ApiKeyModuleConfigurationt = "Configuration/GetModuleConfigurationList";
        public const string ApiKeyCourseContent= "Student/GetCourseContent";
        public const string ApiKeyDiscussionQuestionList = "DiscussionForum/GetDiscussionQuestionList";
        public const string ApiKeyGetDiscussionReplyList = "DiscussionForum/GetDiscussionReplyList";
        public const string ApiKeyGetQuizList = "DigiclassQuiz/GetStudentQuiz";
        public const string ApiKeyStartQuiz = "DigiclassQuiz/StartQuiz";
        public const string ApiKeyAddQuizQuestionAttempt = "DigiclassQuiz/AddQuizQuestionAttempt";
        public const string ApiKeyFinalSubmitQuiz = "DigiclassQuiz/FinalSubmitQuiz";
        public const string ApiKeyQuizResult= "DigiclassQuiz/GetQuizResultByUserId";
        public const string ApiKeyCourseDocuments= "Student/GetCourseDocuments";
        public const string ApiKeyLiveRecording= "ScheduleLiveClass/GetRecordLiveClassListRange";


        public const string ApiKeyHttpAuthorization = "Authorization";
        public const string ApiKeyBearer = "bearer ";
        public const string JSONContentType = "application/json";
        public static bool IsValidEmail = false;
        public const string TabIdKey = "TabId";
        public const string IsLogin = "IsLogin";
        public const string UserId = "UserId";
        public const string RoleId = "RoleId";
    }
}
