using System;
using Newtonsoft.Json;

namespace DeveloperCourse.SecondTask.Image.API.Clients.DTOs
{
    public class ResourceDto
    {
        [JsonProperty("resource_id")]
        public string ResourceId { get; set; }

        [JsonProperty("file")]
        public Uri DownloadLink { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("preview")]
        public Uri PreviewLink { get; set; }

        [JsonProperty("public_url")]
        public Uri PublicLink { get; set; }

        [JsonProperty("path")]
        public string FilePath { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("name")]
        public string FileName { get; set; }

        [JsonProperty("type")]
        public string ResourceType { get; set; }
    }
}