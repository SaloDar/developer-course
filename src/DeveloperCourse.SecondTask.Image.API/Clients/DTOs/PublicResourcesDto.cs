using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeveloperCourse.SecondTask.Image.API.Clients.DTOs
{
    public class PublicResourcesDto
    {
        [JsonProperty("items")]
        public List<ResourceDto> Items { get; set; }
    }
}