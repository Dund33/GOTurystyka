using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public class Admin
    {
        public Admin()
        {
            Points = new HashSet<Point>();
        }

        public int Id { get; set; }
        public string Pesel { get; set; }
        public int WorkerId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool LoggedIn { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Point> Points { get; set; }
    }
}