using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public string? TenSp { get; set; }

    public int? SoLuong { get; set; }

    public decimal? Gia { get; set; }

    public string? Hinh { get; set; }

    public int? MaLoai { get; set; }

    public int? MaNsx { get; set; }

    public int? MaKho { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual Kho? MaKhoNavigation { get; set; }

    public virtual Loai? MaLoaiNavigation { get; set; }

    public virtual NhaSanXuat? MaNsxNavigation { get; set; }
}
