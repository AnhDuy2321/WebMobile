using System.ComponentModel.DataAnnotations;

namespace Web.ModelViews
{
    public class MuaHangVM
    {
        public int MaKh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Họ và Tên")]
        public string TenKh { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Địa chỉ nhận hàng")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Tỉnh/Thành")]
        public int PaymentID { get; set; }
        public string Note { get; set; }
    }
}
