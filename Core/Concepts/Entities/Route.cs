using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concepts.Entities
{
    public class Route
    {
        [Key]
        public int RouteId { get; set; }

        [ForeignKey("StartPoint")]
        public int StartPointId { get; set; }
        public Point StartPoint { get; set; }

        [ForeignKey("EndPoint")]
        public int EndPointId { get; set; }
        public Point EndPoint { get; set; }

        // Additional related information about the travel
        public string TravelDetails { get; set; }

        public Route()
        {
        }

        public Route(Point startPoint, Point endPoint, string travelDetails)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            TravelDetails = travelDetails;
        }
    }

    public class Point
    {
        [Key]
        public int PointId { get; set; }

        public float X { get; set; }
        public float Y { get; set; }

        public Point()
        {
        }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
