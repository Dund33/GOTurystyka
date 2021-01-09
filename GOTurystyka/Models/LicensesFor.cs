﻿using System;
using System.Collections.Generic;

#nullable disable

namespace GOTurystyka.Model
{
    public partial class LicensesFor
    {
        public string AreaName { get; set; }
        public DateTime DateOfLicensing { get; set; }
        public int ForemanId { get; set; }
        public int SegmentId { get; set; }

        public virtual Foreman Foreman { get; set; }
        public virtual Segment Segment { get; set; }
    }
}
