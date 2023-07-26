using System;
using System.Collections.Generic;

namespace Clinic.Models
{
    public partial class Treatment
    {
        public int Tid { get; set; }
        public string Diagnose { get; set; } = null!;
        public string Solution { get; set; } = null!;
        public string? Username { get; set; }

        public virtual Patient TidNavigation { get; set; } = null!;
        public virtual Account? UsernameNavigation { get; set; }
    }
}
