﻿using System;

namespace ART.Gravity.BL.Models
{
    public class Gravity
    {

        // Force = G * (M1 * M2/ r^2)
        public Guid Id { get; set; }
        public DateTime ChangeDate { get; set; }
        public float Mass1 { get; set; }
        public float Mass2 { get; set; }
        public float Distance { get; set; }
        public float Force { get; set; }
    }
}
