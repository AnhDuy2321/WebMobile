using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public DateTime? NgayLap { get; set; }

    public int? Tong { get; set; }

    public int? MaKh { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual KhachHang? MaKhNavigation { get; set; }
}
