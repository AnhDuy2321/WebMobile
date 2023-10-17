using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class NhaSanXuat
{
    public int MaNsx { get; set; }

    public string? TenNsx { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
