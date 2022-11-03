﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModel;
using PJ_MSIT143_team02.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PJ_MSIT143_team02.Controllers
{

    public class RoomController : Controller
    {

        MingSuContext db = new MingSuContext();

        private IWebHostEnvironment _enviro;
        public RoomController(IWebHostEnvironment p)
        {
            _enviro = p;
        }


        public IActionResult List(CKeywordViewModel model)
        {

            MingSuContext db = new MingSuContext();
            IEnumerable<Room> datas = null;
            if (string.IsNullOrEmpty(model.txtKeyword))
                datas = from p in db.Rooms
                        select p;
            else
                datas = db.Rooms.Where(p => p.RoomName.Contains(model.txtKeyword)
                || p.RoomPrice.ToString().Contains(model.txtKeyword)
                || p.RoomIntrodution.Contains(model.txtKeyword)
                || p.MemberId.ToString().Contains(model.txtKeyword)
                || p.RoomstatusId.ToString().Contains(model.txtKeyword)
                || p.Address.Contains(model.txtKeyword)
                || p.CreateDate.ToString().Contains(model.txtKeyword)
                || p.Qty.ToString().Contains(model.txtKeyword));
            return View(datas);
        }

        public IActionResult ListView(CKeywordViewModel model)
        {
            MingSuContext db = new MingSuContext();
            List<CAllViewModel> cAlls = new List<CAllViewModel>();
            var q = from r in db.Rooms
                    select r;
            IEnumerable<Room> datas = null;
            if (string.IsNullOrEmpty(model.txtKeyword))

                datas = from r in db.Rooms
                        join i in db.ImageReferences on r.RoomId equals i.RoomId
                        join k in db.Images on i.ImageId equals k.ImageId
                        join m in db.Members on r.MemberId equals m.MemberId
                        join l in db.RoomStatuses on r.RoomstatusId equals l.RoomstatusId
                        select r;
            else
                datas = db.Rooms.Where(p => p.RoomName.Contains(model.txtKeyword)
                || p.RoomPrice.ToString().Contains(model.txtKeyword)
                || p.RoomIntrodution.Contains(model.txtKeyword)
                || p.MemberId.ToString().Contains(model.txtKeyword)
                || p.RoomstatusId.ToString().Contains(model.txtKeyword)
                || p.Address.Contains(model.txtKeyword)
                || p.CreateDate.ToString().Contains(model.txtKeyword));
            return View(cAlls);
        }

        public IActionResult TestListView(CKeywordViewModel model)
        {
            DateTime thisDate = new DateTime(0001, 1, 1);
            MingSuContext db = new MingSuContext();
            IEnumerable<Room> datas = null;
            if (string.IsNullOrEmpty(model.txtKeyword)) {
                if ((0.Equals(model.txtQty))
                    && (thisDate.Equals(model.mydatein)
                    && thisDate.Equals(model.mydateout)))
                    datas = from r in db.Rooms
                            select r;
                else if (thisDate.Equals(model.mydatein)
                    && thisDate.Equals(model.mydateout))
                    datas = from r in db.Rooms
                            where (r.Qty.Equals(model.txtQty))
                            select r;
                else if (0.Equals(model.txtQty) && thisDate.Equals(model.mydateout))
                    datas = from r in db.Rooms
                            join o in db.OrderDetails on r.RoomId equals o.RoomId
                            into subGrp
                            from s in subGrp.DefaultIfEmpty()
                            where (s.OrderStartDate.Equals(model.mydatein))
                            select r;
                else if (0.Equals(model.txtQty) && thisDate.Equals(model.mydatein))
                    datas = from r in db.Rooms
                            join o in db.OrderDetails on r.RoomId equals o.RoomId
                            into subGrp
                            from s in subGrp.DefaultIfEmpty()
                            where (s.OrderEndDate.Equals(model.mydateout))
                            select r;
                else if (0.Equals(model.txtQty))
                    datas = from r in db.Rooms
                            join o in db.OrderDetails on r.RoomId equals o.RoomId
                            into subGrp
                            from s in subGrp.DefaultIfEmpty()
                            where (s.OrderStartDate.Equals(model.mydatein)
                            && s.OrderEndDate.Equals(model.mydateout))
                            select r;
            }
            else {
                if ((0.Equals(model.txtQty))
                    && thisDate.Equals(model.mydatein)
                    && thisDate.Equals(model.mydateout))
                    datas = from r in db.Rooms
                            where (r.RoomName.Contains(model.txtKeyword)
                            || r.RoomPrice.ToString().Contains(model.txtKeyword)
                            || r.RoomIntrodution.Contains(model.txtKeyword)
                            || r.MemberId.ToString().Contains(model.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(model.txtKeyword)
                            || r.Address.Contains(model.txtKeyword))
                            select r;
                else if (thisDate.Equals(model.mydatein)
                    && thisDate.Equals(model.mydateout))
                    datas = from r in db.Rooms
                            where (r.RoomName.Contains(model.txtKeyword)
                            || r.RoomPrice.ToString().Contains(model.txtKeyword)
                            || r.RoomIntrodution.Contains(model.txtKeyword)
                            || r.MemberId.ToString().Contains(model.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(model.txtKeyword)
                            || r.Address.Contains(model.txtKeyword)
                            || r.Qty.Equals(model.txtQty))
                            select r;
                else if (0.Equals(model.txtQty) && thisDate.Equals(model.mydateout))
                    datas = from r in db.Rooms
                            join o in db.OrderDetails on r.RoomId equals o.RoomId
                            into subGrp
                            from s in subGrp.DefaultIfEmpty()
                            where (r.RoomName.Contains(model.txtKeyword)
                            || r.RoomPrice.ToString().Contains(model.txtKeyword)
                            || r.RoomIntrodution.Contains(model.txtKeyword)
                            || r.MemberId.ToString().Contains(model.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(model.txtKeyword)
                            || r.Address.Contains(model.txtKeyword)
                            || s.OrderEndDate.Equals(model.mydatein))
                            select r;
                else if (0.Equals(model.txtQty) && thisDate.Equals(model.mydatein))
                    datas = from r in db.Rooms
                            join o in db.OrderDetails on r.RoomId equals o.RoomId
                            into subGrp
                            from s in subGrp.DefaultIfEmpty()
                            where (r.RoomName.Contains(model.txtKeyword)
                            || r.RoomPrice.ToString().Contains(model.txtKeyword)
                            || r.RoomIntrodution.Contains(model.txtKeyword)
                            || r.MemberId.ToString().Contains(model.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(model.txtKeyword)
                            || r.Address.Contains(model.txtKeyword)
                            || s.OrderEndDate.Equals(model.mydateout))
                            select r;
                else if (0.Equals(model.txtQty))
                    datas = from r in db.Rooms
                            join o in db.OrderDetails on r.RoomId equals o.RoomId
                            into subGrp
                            from s in subGrp.DefaultIfEmpty()
                            where (r.RoomName.Contains(model.txtKeyword)
                            || r.RoomPrice.ToString().Contains(model.txtKeyword)
                            || r.RoomIntrodution.Contains(model.txtKeyword)
                            || r.MemberId.ToString().Contains(model.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(model.txtKeyword)
                            || r.Address.Contains(model.txtKeyword)
                            || (s.OrderStartDate.Equals(model.mydatein)
                            && s.OrderEndDate.Equals(model.mydateout)))
                            select r;
                else
                    datas = from r in db.Rooms
                            join o in db.OrderDetails on r.RoomId equals o.RoomId
                            into subGrp
                            from s in subGrp.DefaultIfEmpty()
                            where (r.RoomName.Contains(model.txtKeyword)
                            || r.RoomPrice.ToString().Contains(model.txtKeyword)
                            || r.RoomIntrodution.Contains(model.txtKeyword)
                            || r.MemberId.ToString().Contains(model.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(model.txtKeyword)
                            || r.Address.Contains(model.txtKeyword)
                            || r.Qty.Equals(model.txtQty)
                            || s.OrderStartDate.Equals(model.mydatein)
                            || s.OrderEndDate.Equals(model.mydateout))
                            select r;
            }
            //var datas = ListAll.ToList().ToPagedList(page, pageSize);
            return View(datas);
        }
        public IActionResult AddRoom(CKeywordViewModel model)
        {
            return View();

        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Room p)
        {
            MingSuContext db = new MingSuContext();
            db.Rooms.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                MingSuContext db = new MingSuContext();
                Room r = db.Rooms.FirstOrDefault(t => t.RoomId == id);
                if (r != null)
                {
                    db.Rooms.Remove(r);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                MingSuContext db = new MingSuContext();
                Room r = db.Rooms.FirstOrDefault(t => t.RoomId == id);
                if (r != null)
                    return View(r);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(CRoomViewModel InRoom)
        {

            MingSuContext db = new MingSuContext();
            Room r = db.Rooms.FirstOrDefault(t => t.RoomId == InRoom.RoomId);
            if (r != null)
            {
                if (InRoom.photo != null)
                {
                    string pName = Guid.NewGuid().ToString() + ".jpg";
                    r.FimagePath = pName;
                    string path = _enviro.WebRootPath + "/image/" + pName;
                    InRoom.photo.CopyTo(new FileStream(path, FileMode.Create));
                }


                r.RoomId = InRoom.RoomId;
                r.RoomName = InRoom.RoomName;
                r.RoomPrice = InRoom.RoomPrice;
                r.RoomIntrodution = InRoom.RoomIntrodution;
                r.MemberId = InRoom.MemberId;
                r.RoomstatusId = InRoom.RoomstatusId;
                r.Address = InRoom.Address;
                r.CreateDate = InRoom.CreateDate;
                r.Qty = InRoom.Qty;
                r.FimagePath = InRoom.FimagePath;
                db.SaveChanges();
            }
            return RedirectToAction("List", "path");
        }
        public IActionResult Details(int? Id)
        {
            if (Id != null)
            {
                MingSuContext db = new MingSuContext();
                IEnumerable<Room> r = db.Rooms.Where(r => r.RoomId == Id);
                if (r != null)                    
                    return View(r.ToList());
            }
            return RedirectPermanent("TestListView");
        }
        //public static byte[] GetBytesFromImage(string filename)
        //{
        //    try
        //    {
        //        FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
        //        int length = (int)fs.Length;
        //        byte[] image = new byte[length];
        //        fs.Read(image, 0, length);
        //        fs.Close();
        //        return image;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //public FileResult Image()
        //{
        //    byte[] image = GetBytesFromDB();
        //    return new FileContentResult(image, "image/jpeg");
        //}

    }
}
