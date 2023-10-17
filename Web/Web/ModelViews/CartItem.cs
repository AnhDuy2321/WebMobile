using Web.Models;

namespace Web.ModelViews
{
    public class CartItem
    {
        public SanPham sanpham { get; set; }
        public int amount { get; set; }
        public double TotalMoney => amount * sanpham.Gia.Value;
    }
}
