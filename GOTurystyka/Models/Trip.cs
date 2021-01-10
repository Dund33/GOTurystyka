using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public class Trip
    {
        public Trip()
        {
            UsersInTrips = new HashSet<UsersInTrip>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool Ended { get; set; }
        public bool Confirmed { get; set; }
        public int RouteId { get; set; }
        public int TouristId { get; set; }

        public virtual Route Route { get; set; }
        public virtual Tourist Tourist { get; set; }
        public virtual ICollection<UsersInTrip> UsersInTrips { get; set; }
    }
}