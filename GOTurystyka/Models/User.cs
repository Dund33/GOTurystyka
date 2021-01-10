using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public partial class User
    {
        public User()
        {
            UsersInTrips = new HashSet<UsersInTrip>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool LoggedIn { get; set; }
        public string Email { get; set; }

        public virtual ICollection<UsersInTrip> UsersInTrips { get; set; }
    }
}
