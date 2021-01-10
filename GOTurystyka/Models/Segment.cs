using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public partial class Segment
    {
        public Segment()
        {
            LicensesFors = new HashSet<LicensesFor>();
            PointsInSegments = new HashSet<PointsInSegment>();
            SegmentsInRoutes = new HashSet<SegmentsInRoute>();
        }

        public int Id { get; set; }
        public int Points { get; set; }
        public int Length { get; set; }
        public bool HasPoints { get; set; }
        public int PointsDir1 { get; set; }
        public int PointsDir2 { get; set; }
        public int ForemanId { get; set; }
        public int? LicenseForId { get; set; }

        public virtual Foreman Foreman { get; set; }
        public virtual ICollection<LicensesFor> LicensesFors { get; set; }
        public virtual ICollection<PointsInSegment> PointsInSegments { get; set; }
        public virtual ICollection<SegmentsInRoute> SegmentsInRoutes { get; set; }
    }
}
