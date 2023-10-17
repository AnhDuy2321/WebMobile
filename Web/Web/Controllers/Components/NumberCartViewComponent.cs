using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Extension;
using Web.ModelViews;

namespace Web.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            return View(cart);
        }
    }
}
