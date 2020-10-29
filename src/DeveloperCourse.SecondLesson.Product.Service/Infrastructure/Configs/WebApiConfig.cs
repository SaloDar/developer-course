using System;

namespace DeveloperCourse.SecondLesson.Product.Service.Infrastructure.Configs
{
    public class WebApiConfig
    {
        public string ServiceName { get; set; }

        public ApiRoutes Routes { get; set; }
    }

    public class ApiRoutes
    {
        public Uri PriceApi { get; set; }

        public Uri ImageApi { get; set; }
    }
}