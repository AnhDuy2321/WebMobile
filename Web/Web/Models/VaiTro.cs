using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class VaiTro
{
    public int MaVaiTro { get; set; }

    public string? VaiTro1 { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
