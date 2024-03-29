﻿using System;

#nullable disable

namespace GOTurystyka.Models
{
    public class TemporaryPoint
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