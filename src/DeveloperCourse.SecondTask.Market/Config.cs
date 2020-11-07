using System;

namespace DeveloperCourse.ThirdLesson.View
{
    public static class Config
    {
        public static string ApiBase => "http://localhost:4300/api";

        public static string ProductBase => ApiBase + "/product";
    }
}