using PJ_MSIT143_team02.Models;
using System.Linq;

namespace PJ_MSIT143_team02.Services
{
    public class CartService
    {
        public Discount queryCoupon(CCartCartItem couponItem) {
            MingSuContext db = new MingSuContext();
            var discount = db.Discounts.SingleOrDefault(x => x.Coupon.Equals(couponItem.Coupon));
            return discount;
        }
    }
}
