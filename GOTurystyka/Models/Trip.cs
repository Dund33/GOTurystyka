using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public partial class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool Ended { get; set; }
        public bool Confirmed { get; set; }
        public int RouteId { get; set; }

        public virtual Route Route { get; set; }
    }
}
