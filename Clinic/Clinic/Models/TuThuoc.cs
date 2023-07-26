using System;
using System.Collections.Generic;

namespace Clinic.Models
{
    public partial class TuThuoc
    {
        public TuThuoc()
        {
            DonThuocs = new HashSet<DonThuoc>();
        }

        public int ThuocId { get; set; }
        public string TenThuoc { get; set; } = null!;
        public string HangSanXuat { get; set; } = null!;
        public DateTime NgaySx { get; set; }
        public DateTime HanSx { get; set; }
        public int? LoaiId { get; set; }

        public virtual LoaiThuoc? Loai { get; set; }
        public virtual ICollection<DonThuoc> DonThuocs { get; set; }
    }
}
