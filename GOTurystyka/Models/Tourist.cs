using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public class Tourist
    {
        public Tourist()
        {
            Routes = new HashSet<Route>();
            TemporaryPoints = new HashSet<TemporaryPoint>();
            TouristGots = new HashSet<TouristGot>();
            Trips = new HashSet<Trip>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool LoggedIn { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<TemporaryPoint> TemporaryPoints { get; set; }
        public virtual ICollection<TouristGot> TouristGots { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}