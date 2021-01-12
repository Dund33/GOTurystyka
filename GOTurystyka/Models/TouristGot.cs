using System;

#nullable disable

namespace GOTurystyka.Models
{
    public class TouristGot
    {
        public int Id { get; set; }
        public DateTime? AwardedOn { get; set; }
        public bool Awarded { get; set; }
        public int? Got4id { get; set; }
        public int? Got3id { get; set; }
        public int CommisionId { get; set; }
        public int TouristId { get; set; }

        public virtual Commission Commision { get; set; }
        public virtual Got3 Got3 { get; set; }
        public virtual Got4 Got4 { get; set; }
        public virtual Tourist Tourist { get; set; }
    }
}