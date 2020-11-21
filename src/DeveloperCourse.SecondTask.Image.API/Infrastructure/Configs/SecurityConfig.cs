using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DeveloperCourse.SecondTask.Image.API.Infrastructure.Configs
{
    public class SecurityConfig
    {
        public List<string> Audiences { get; set; }

        public string Issuer { get; set; }

        public string SigningKey { get; set; }

        public string EncryptionKey { get; set; }

        [JsonIgnore]
        public byte[] SigningKeyBytes => Encoding.UTF8.GetBytes(SigningKey);

        [JsonIgnore]
        public byte[] EncryptionKeyBytes => Encoding.UTF8.GetBytes(EncryptionKey);
    }
}