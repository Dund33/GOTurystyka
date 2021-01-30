using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        [DisplayName("Number of points")]
        public int NumberOfPoints { get; set; }
        public bool AlreadyTravelled { get; set; }
        [DisplayName("Created on")]
        public DateTime DateOfCreation { get; set; }
        [DisplayName("Last updated on")]
        public DateTime LastUpdate { get; set; }
        public bool Approved { get; set; }
        public int CreatorId { get; set; }
        [DisplayName("Waiting for approval")]
        public bool WaitingForApproval { get; set; }

        public virtual Tourist Creator { get; set; }
        public virtual ICollection<SegmentsInRoute> SegmentsInRoutes { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}