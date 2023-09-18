using CULMS.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CULMS.Helpers
{
    public class Utilities
    {
        public static string  Token { get; set; }
        public static bool  IsComeFromEnrollCourse { get; set; }
        public static int TotalLogoutTime { get; set; } = 2;
        public static double savedQuizTime { get; set; }
        public static List<AllCourseData> SavedAllCourseList { get; set; }
        public static int courseId { get; set; }
        public static int courseQuizId { get; set; }
        public static int DiscussionCourseId { get; set; }
        public static int courseModuleId { get; set; }
        public static int ContentType { get; set; }
        public static ValidateTokenResponseModel SavedProfileData { get; set; }
    }
}
