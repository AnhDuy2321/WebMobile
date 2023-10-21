using Web.Models;

namespace Web.ModelViews
{
    public class XemDonHang
    {
        public HoaDon hoadon { get; set; }
        public List<ChiTietHoaDon> ChiTietDonHang { get; set; }
    }
}
