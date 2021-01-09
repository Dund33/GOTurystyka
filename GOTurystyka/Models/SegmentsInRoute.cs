using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Model
{
    public partial class SegmentsInRoute
    {
        public int RouteId { get; set; }
        public int SegmentId { get; set; }

        public virtual Route Route { get; set; }
        public virtual Segment Segment { get; set; }
    }
}
