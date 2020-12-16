using DeveloperCourse.ThirdTask.Weather.API.Types;

namespace DeveloperCourse.ThirdTask.Weather.API.Services.DTOs
{
    public class CityWind
    {
        /// <summary>
        /// City name.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Wind speed in meter/sec.
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Wind direction.
        /// North, Northeast, East, Southeast,
        /// South, Southwest, West, Northwest.
        /// </summary>
        public CardinalDirection Direction { get; set; }

        /// <summary>
        /// Wind direction in degrees.
        /// </summary>
        public uint DirectionDegrees { get; set; }

        /// <summary>
        /// Unit of speed.
        /// meter/sec, miles/hour.
        /// </summary>
        public SpeedUnit Metric { get; set; }
    }
}