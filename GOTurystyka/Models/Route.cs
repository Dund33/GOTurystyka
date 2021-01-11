using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace GOTurystyka.Models
{
    public partial class Route
    {
        public Route()
        {
            SegmentsInRoutes = new HashSet<SegmentsInRoute>();
            Trips = new HashSet<Trip>();
            SegmentsInRoutes.Add(new SegmentsInRoute
            {
                RouteId = 3,
                OrderingNumber = 1,
                SegmentId = 1
            });
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Length { get; set; }
        public int NumberOfPoints { get; set; }
        public bool AlreadyTravelled { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Approved { get; set; }
        [DisplayName("Creator")]
        public int CreatorId { get; set; }

        public virtual Tourist Creator { get; set; }
        public virtual ICollection<SegmentsInRoute> SegmentsInRoutes { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
