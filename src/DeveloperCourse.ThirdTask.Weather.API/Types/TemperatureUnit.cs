using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DeveloperCourse.ThirdTask.Weather.API.Types
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TemperatureUnit
    {
        Kelvin = 0,

        Celsius = 1,

        Fahrenheit = 2
    }
}