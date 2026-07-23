using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.ViewModels;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class DiscountController : Controller
    {
        private DiscountService _service;

        public DiscountService discountService { 
            get {
                _service = HttpContext.RequestServices.GetService(typeof(DiscountService)) as DiscountService;
                return _service;
            } 
        }

        public IActionResult DiscountMain()
        {
            var data = discountService.queryAll();
            return View(data);
        }

        public IActionResult Details(int? Id)
        {
            if (Id != null)
            {
                var d = discountService.details(Id);
                if (d != null)
                    return View(d.ToList());
            }
            return RedirectPermanent("DiscountMain");
        }

        public IActionResult DiscountAdmin()
        {
            var data = discountService.queryAll();
            return View(data);
        }
        [HttpPost]
        public IActionResult DiscountAdmin(DisViewModel model)
        {
            IEnumerable<Discount> data;
            if (string.IsNullOrEmpty(model.txtKey))
                data = discountService.queryAll();
            else
                data = discountService.query(model);
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Discount d)
        {
            if (string.IsNullOrEmpty(d.DiscountInfo) || string.IsNullOrEmpty(d.DiscountName)
                || string.IsNullOrEmpty(d.DiscountValue.ToString()))
                return View();
            discountService.create(d);
            return RedirectPermanent("DiscountAdmin");
        }

        public IActionResult Delete(int? Id)
        {
            discountService.delete(Id);
            return RedirectToAction("DiscountAdmin");
        }

        public IActionResult Edit(int? Id)
        {
            if (Id != null)
            {
                Discount d = discountService.editById(Id);
                if (d != null)
                    return View(d);
            }
            return RedirectPermanent("DiscountAdmin");
        }
        [HttpPost]
        public IActionResult Edit(DisViewModel input)
        {
            discountService.edit(input);
            return RedirectToAction("DiscountAdmin");
        }
    }
}
