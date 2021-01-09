using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Model
{
    public partial class PointsInSegment
    {
        public int SegmentId { get; set; }
        public int PointId { get; set; }

        public virtual Point Point { get; set; }
        public virtual Segment Segment { get; set; }
    }
}
