using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public class Got4
    {
        public Got4()
        {
            TouristGots = new HashSet<TouristGot>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public int PointsRequired { get; set; }
        public int TierId { get; set; }

        public virtual Gottier Tier { get; set; }
        public virtual ICollection<TouristGot> TouristGots { get; set; }
    }
}