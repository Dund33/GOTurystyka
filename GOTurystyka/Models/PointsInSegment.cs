#nullable disable

namespace GOTurystyka.Models
{
    public partial class PointsInSegment
    {
        public int SegmentId { get; set; }
        public int PointId { get; set; }

        public virtual Point Point { get; set; }
        public virtual Segment Segment { get; set; }
    }
}
