using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PJ_MSIT143_team02.Services
{
    public class DiscountService
    {
        public IQueryable<Discount> queryAll()
        {
            var data = from d in (new MingSuContext()).Discounts
                       select d;
            return data;
        }

        public IQueryable<Discount> query(DisViewModel model)
        {
            var data = from d in (new MingSuContext()).Discounts
                       where (d.DiscountInfo.Contains(model.txtKey) ||
                       d.DiscountName.Contains(model.txtKey) ||
                       d.DiscountValue.ToString().Contains(model.txtKey) ||
                       d.Coupon.Contains(model.txtKey))
                       select d;
            return data;
        }

        public List<Discount> queryTop3() {
            var data = (from d in (new MingSuContext()).Discounts
                        select new Discount
                        {
                            RoomDiscountId = d.RoomDiscountId,
                            DiscountName = d.DiscountName
                        }).Take(3).ToList();
            return data;
        }

        public void create(Discount d)
        {
            MingSuContext db = new MingSuContext();
            db.Discounts.Add(d);
            db.SaveChanges();            
        }

        public void edit(DisViewModel input)
        {
            MingSuContext db = new MingSuContext();
            Discount d = db.Discounts.FirstOrDefault(d => d.RoomDiscountId == input.Id);
            if (d != null)
            {
                d.DiscountInfo = input.DiscountInfo;
                d.DiscountName = input.DiscountName;
                d.DiscountValue = input.DiscountValue;
                d.Coupon = input.Coupon;
                db.SaveChanges();
            }
        }

        public Discount editById(int? Id)
        {
            MingSuContext db = new MingSuContext();
            Discount d = db.Discounts.FirstOrDefault(d => d.RoomDiscountId == Id);
            return d;
        }

        public void delete(int? Id)
        {
            MingSuContext db = new MingSuContext();
            Discount d = db.Discounts.FirstOrDefault(d => d.RoomDiscountId == Id);
            if (d != null)
            {
                db.Discounts.Remove(d);
                db.SaveChanges();
            }
        }

        public IQueryable<Discount> details(int? Id) {
            MingSuContext db = new MingSuContext();
            var d = db.Discounts.Where(d => d.RoomDiscountId == Id);
            return d;
        }
    }
}
