using System;
using System.Collections.Generic;

namespace Clinic.Models
{
    public partial class Service
    {
        public Service()
        {
            Pids = new HashSet<Patient>();
        }

        public int Sid { get; set; }
        public string Sname { get; set; } = null!;
        public double Price { get; set; }

        public virtual ICollection<Patient> Pids { get; set; }
    }
}
