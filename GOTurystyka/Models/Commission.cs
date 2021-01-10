using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public partial class Commission
    {
        public Commission()
        {
            TouristGots = new HashSet<TouristGot>();
        }

        public int Id { get; set; }
        public int CommissionNumber { get; set; }
        public DateTime TakenTheOfficeDate { get; set; }
        public DateTime QuitOfficeDate { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool LoggedIn { get; set; }
        public string Email { get; set; }

        public virtual ICollection<TouristGot> TouristGots { get; set; }
    }
}
