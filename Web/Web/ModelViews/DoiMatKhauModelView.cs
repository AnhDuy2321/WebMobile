using System.ComponentModel.DataAnnotations;

namespace Web.ModelViews
{
    public class DoiMatKhauModelView
    {
        [Key]
        public int MaKh { get; set; }

        [Display(Name = "Mật khẩu hiện tại")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại")]
        public string MatKhauHienTai { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
        [MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu tối thiểu 5 ký tự")]
        public string MatKhau { get; set; }

        [MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu tối thiểu 5 ký tự")]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Nhập lại mật khẩu không đúng")]
        public string XacNhanMatKhau { get; set; }
    }
}
