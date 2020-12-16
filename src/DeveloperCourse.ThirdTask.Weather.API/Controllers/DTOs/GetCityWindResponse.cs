namespace DeveloperCourse.ThirdTask.Weather.API.Controllers.DTOs
{
    public class GetCityWindResponse
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
        public string Direction { get; set; }

        /// <summary>
        /// Wind direction in degrees.
        /// </summary>
        public uint DirectionDegrees { get; set; }
        
        /// <summary>
        /// Unit of speed.
        /// meter/sec, miles/hour.
        /// </summary>
        public string Metric { get; set; }
    }
}