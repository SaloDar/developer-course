using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DeveloperCourse.SecondTask.Image.API.Infrastructure.Configs
{
    public class YandexDiskConfig
    {
        public Uri BaseUrl { get; set; }

        public string AccessToken { get; set; }

        public string AccessScheme { get; set; }

        public string BasePath { get; set; }

        [JsonIgnore]
        public AuthenticationHeaderValue AuthenticationHeader =>
            new AuthenticationHeaderValue(AccessScheme, AccessToken);
    }
}