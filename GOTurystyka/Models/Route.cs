using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public class Route
    {
        public Route()
        {
            SegmentsInRoutes = new HashSet<SegmentsInRoute>();
            Trips = new HashSet<Trip>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public int NumberOfPoints { get; set; }
        public bool AlreadyTravelled { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Approved { get; set; }
        public int CreatorId { get; set; }

        public virtual Tourist Creator { get; set; }
        public virtual ICollection<SegmentsInRoute> SegmentsInRoutes { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}