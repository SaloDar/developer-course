using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DeveloperCourse.SecondTask.Identity.API.Infrastructure.Configs
{
    public class SecurityConfig
    {
        public List<string> Audiences { get; set; }

        public string Issuer { get; set; }

        public string SigningKey { get; set; }

        public string EncryptionKey { get; set; }

        public TimeSpan ExpirationTime { get; set; }
        
        [JsonIgnore]
        public string SigningKeyAlgorithm => SecurityAlgorithms.HmacSha256;
        
        [JsonIgnore]
        public string EncryptionKeyAlgorithm => SecurityAlgorithms.Aes256CbcHmacSha512;

        [JsonIgnore]
        public byte[] SigningKeyBytes => Encoding.UTF8.GetBytes(SigningKey);

        [JsonIgnore]
        public byte[] EncryptionKeyBytes => Encoding.UTF8.GetBytes(EncryptionKey);
    }
}