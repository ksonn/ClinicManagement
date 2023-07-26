using System;
using System.Collections.Generic;
using System.Linq;

namespace Clinic.Models
{
    public partial class Patient
    {
        public Patient()
        {
            DonThuocs = new HashSet<DonThuoc>();
            Sids = new HashSet<Service>();
        }

        public int Pid { get; set; }
        public string Pname { get; set; } = null!;
        public bool Gender { get; set; }
        public DateTime Dob { get; set; }
        public string? Job { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Cmnd { get; set; }
        public DateTime Dayadd { get; set; }
        public string Reason { get; set; } = null!;
        public string? Photo { get; set; }

        public virtual Treatment? Treatment { get; set; }
        public virtual ICollection<DonThuoc> DonThuocs { get; set; }

        public virtual ICollection<Service> Sids { get; set; }


    }
}
