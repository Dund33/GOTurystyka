using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Models
{
    public partial class Got4
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int PointsRequired { get; set; }
        public int TierId { get; set; }

        public virtual Gottier Tier { get; set; }
    }
}
