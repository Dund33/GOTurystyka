#nullable disable

namespace GOTurystyka.Models
{
    public class PointsInSegment
    {
        public int SegmentId { get; set; }
        public int PointId { get; set; }
        public int OrderingNumber { get; set; }

        public virtual Point Point { get; set; }
        public virtual Segment Segment { get; set; }
    }
}