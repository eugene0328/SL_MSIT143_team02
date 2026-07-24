using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.Services;
using System.Threading.Tasks;
using System.Linq;

namespace PJ_MSIT143_team02.ViewComponents
{
    public class DiscountViewComponent : ViewComponent
    {
        private DiscountService _service;

        public DiscountService discountService
        {
            get
            {
                _service = HttpContext.RequestServices.GetService(typeof(DiscountService)) as DiscountService;
                return _service;
            }
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = discountService.queryTop3();
            return View(data);
        }
    }
}
