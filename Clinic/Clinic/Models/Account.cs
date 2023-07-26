using System;
using System.Collections.Generic;

namespace Clinic.Models
{
    public partial class Account
    {
        public Account()
        {
            Treatments = new HashSet<Treatment>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;

        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
