using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public partial class Foreman
    {
        public Foreman()
        {
            LicensesFors = new HashSet<LicensesFor>();
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
        public bool Active { get; set; }

        public virtual ICollection<LicensesFor> LicensesFors { get; set; }
    }
}
