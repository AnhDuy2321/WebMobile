using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Kho
{
    public int MaKho { get; set; }

    public string? TenSp { get; set; }

    public int? SoLuong { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
