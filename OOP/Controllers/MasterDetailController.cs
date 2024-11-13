using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OOP.Models;
using System.Net;
using System.Web.Mvc;
using System.Globalization;

namespace OOP.Controllers
{
    public class MasterDetailController : Controller
    {
        // GET: MasterDetail
        Models.DoAnOOPEntities2 db = new Models.DoAnOOPEntities2();

        public ActionResult Index()
        {
            var listOfData = db.Reservation.ToList();

            return View(listOfData);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var menuidList = db.Menu.Select(x => new SelectListItem { Value = x.menu_id.ToString(), Text = x.menu_id }).ToList();
            ViewBag.MenuIdList = new SelectList(menuidList, "Value", "Text");
     

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Reservation model)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu được gửi từ form có hợp lệ không
            {
                try
                {


                    var menuid = db.Menu.Find(model.menu_id);


                    if (menuid == null)
                    {
                        ModelState.AddModelError("menu_id", "Mã hoa không hợp lệ.");
                    }

                 

                    if (ModelState.IsValid) // Kiểm tra xem sau khi kiểm tra mã hoa và mã khu vực, ModelState có còn hợp lệ không
                    {
                        // Thêm model vào cơ sở dữ liệu
                        db.Reservation.Add(model);
                        db.SaveChanges();

                        ViewBag.Message = "Data inserted successfully";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error occurred: " + ex.Message;
                }
            }
            var menuidList = db.Menu.Select(x => new SelectListItem { Value = x.menu_id.ToString(), Text = x.menu_id }).ToList();
            ViewBag.MenuIdList = new SelectList(menuidList, "Value", "Text");
           
           ;

            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm reservation theo id
            Models.Reservation reservation = db.Reservation.Find(id);

            if (reservation == null)
            {
                return HttpNotFound();
            }

            // Tạo danh sách menu id
            var menuidList = db.Menu.Select(x => new SelectListItem { Value = x.menu_id.ToString(), Text = x.menu_id }).ToList();
            ViewBag.MenuIdList = new SelectList(menuidList, "Value", "Text");
            

            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Reservation model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Cập nhật dữ liệu vào cơ sở dữ liệu
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.Message = "Data updated successfully";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error occurred: " + ex.Message;
                }
            }

            var menuidList = db.Menu.Select(x => new SelectListItem { Value = x.menu_id.ToString(), Text = x.menu_id }).ToList();
            ViewBag.MenuIdList = new SelectList(menuidList, "Value", "Text");
            

            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm reservation theo id
            Models.Reservation reservation = db.Reservation.Find(id);

            if (reservation == null)
            {
                return HttpNotFound();
            }

            return View(reservation);
        }

        // POST: MasterDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Models.Reservation reservation = db.Reservation.Find(id);
                if (reservation != null)
                {
                    db.Reservation.Remove(reservation);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error occurred: " + ex.Message;
                return View();
            }
        }
    }
}