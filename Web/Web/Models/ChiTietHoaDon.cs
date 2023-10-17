using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class ChiTietHoaDon
{
    public int MaCthd { get; set; }

    public int? MaHd { get; set; }

    public int? MaSp { get; set; }

    public int? SoLuong { get; set; }

    public int? DonGia { get; set; }

    public int? ThanhTien { get; set; }

    public virtual HoaDon? MaHdNavigation { get; set; }

    public virtual SanPham? MaSpNavigation { get; set; }
}
