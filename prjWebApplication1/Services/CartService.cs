using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModels;
using System.Linq;

namespace PJ_MSIT143_team02.Services
{
    public class CartService
    {
        public 房源及會員 roomAndMember { get; set; }

        public Discount queryCoupon(CCartCartItem couponItem) {
            MingSuContext db = new MingSuContext();
            var discount = db.Discounts.SingleOrDefault(x => x.Coupon.Equals(couponItem.Coupon));
            if (discount != null)
                roomAndMember = new 房源及會員()
                {
                    DisPrice = couponItem.DisPrice * discount.DiscountValue,
                    Discount = discount,
                };
            return discount;
        }
    }
}
