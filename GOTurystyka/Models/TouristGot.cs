using System;

#nullable disable

namespace GOTurystyka.Models
{
    public partial class TouristGot
    {
        public int Id { get; set; }
        public DateTime? AwardedOn { get; set; }
        public bool Awarded { get; set; }
        public int Gotid { get; set; }
        public int CommisionId { get; set; }
        public int TouristId { get; set; }

        public virtual Commission Commision { get; set; }
        public virtual Got3 Got { get; set; }
        public virtual Tourist Tourist { get; set; }
    }
}
