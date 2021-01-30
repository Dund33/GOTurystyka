using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace GOTurystyka.Models
{
    public class Segment
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
        [DisplayName("Score regulated by the commission")]
        public bool HasPoints { get; set; }
        [DisplayName("Points in dir 1")]
        public int PointsDir1 { get; set; }
        [DisplayName("Points in dir 2")]
        public int PointsDir2 { get; set; }
        public int? ForemanId { get; set; }
        public int? LicenseForId { get; set; }
        public bool Approved { get; set; }
        public int CreatorId { get; set; }
        public bool AlreadyTravelled { get; set; }
        public bool WaitingForApproval { get; set; }

        public virtual Foreman Foreman { get; set; }
        public virtual Tourist Creator { get; set; }
        public virtual ICollection<LicensesFor> LicensesFors { get; set; }
        public virtual ICollection<PointsInSegment> PointsInSegments { get; set; }
        public virtual ICollection<SegmentsInRoute> SegmentsInRoutes { get; set; }
    }
}