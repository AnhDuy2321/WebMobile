using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class KhachHang
{
    public int MaKh { get; set; }

    public string? TenKh { get; set; }

    public string? DiaChi { get; set; }

    public string? Sdt { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? TaiKhoan { get; set; }

    public string? MatKhau { get; set; }

    public string? Salt { get; set; }

    public bool? TrangThai { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
