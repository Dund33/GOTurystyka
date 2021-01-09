using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Model
{
    public partial class TemporaryPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float Height { get; set; }
        public bool? Used { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime GccheckTime { get; set; }
        public int AuthorId { get; set; }

        public virtual Tourist Author { get; set; }
    }
}
