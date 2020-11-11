using System;
using Newtonsoft.Json;

namespace DeveloperCourse.SecondTask.Image.API.Clients.DTOs
{
    public class ResourceLinkDto
    {
        [JsonProperty("operation_id")]
        public string OperationId { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("templated")]
        public string Templated { get; set; }
    }
}