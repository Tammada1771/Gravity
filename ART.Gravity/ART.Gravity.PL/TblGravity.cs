using System;
using System.Collections.Generic;

#nullable disable

namespace ART.Gravity.PL
{
    public partial class tblGravity
    {
        public Guid Id { get; set; }
        public DateTime ChangeDate { get; set; }
        public double MassOne { get; set; }
        public double MassTwo { get; set; }
        public double Distance { get; set; }
        public double Force { get; set; }
    }
}
