using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class TaiKhoan
{
    public int MaTaiKhoan { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public string? MatKhau { get; set; }

    public int MaVaiTro { get; set; }

    public string? Salt { get; set; }

    public virtual VaiTro MaVaiTroNavigation { get; set; } = null!;
}
