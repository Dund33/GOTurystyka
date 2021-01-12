using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public class Point
    {
        public Point()
        {
            PointsInSegments = new HashSet<PointsInSegment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float Height { get; set; }
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual ICollection<PointsInSegment> PointsInSegments { get; set; }
    }
}