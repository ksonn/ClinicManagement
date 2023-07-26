using System;
using System.Collections.Generic;

namespace Clinic.Models
{
    public partial class LoaiThuoc
    {
        public LoaiThuoc()
        {
            TuThuocs = new HashSet<TuThuoc>();
        }

        public int LoaiId { get; set; }
        public string TenLoai { get; set; } = null!;

        public virtual ICollection<TuThuoc> TuThuocs { get; set; }
    }
}
