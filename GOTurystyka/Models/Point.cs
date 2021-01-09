﻿using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Model
{
    public partial class Point
    {
        public Point()
        {
            PointsInSegments = new HashSet<PointsInSegment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float Height { get; set; }

        public virtual ICollection<PointsInSegment> PointsInSegments { get; set; }
    }
}
