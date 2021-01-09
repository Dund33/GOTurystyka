using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public partial class UsersInTrip
    {
        public int UserId { get; set; }
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }
        public virtual User User { get; set; }
    }
}
