using System;
using System.Collections.Generic;

namespace Clinic.Models
{
    public partial class DonThuoc
    {
        public int DonThuocId { get; set; }
        public int Pid { get; set; }
        public int ThuocId { get; set; }

        public virtual Patient PidNavigation { get; set; } = null!;
        public virtual TuThuoc Thuoc { get; set; } = null!;
    }
}
