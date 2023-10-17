using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Loai
{
    public int MaLoai { get; set; }

    public string? TenLoai { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
