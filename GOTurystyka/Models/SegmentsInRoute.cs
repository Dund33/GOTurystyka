#nullable disable

namespace GOTurystyka.Models
{
    public partial class SegmentsInRoute
    {
        public int RouteId { get; set; }
        public int SegmentId { get; set; }

        public virtual Route Route { get; set; }
        public virtual Segment Segment { get; set; }
    }
}
