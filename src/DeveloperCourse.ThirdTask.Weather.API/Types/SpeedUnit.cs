using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DeveloperCourse.ThirdTask.Weather.API.Types
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpeedUnit
    {
        MeterSec = 0,

        MilesHour = 1
    }
}