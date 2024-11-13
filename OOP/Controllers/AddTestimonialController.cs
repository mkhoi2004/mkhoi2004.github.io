using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using OOP.Models;
namespace OOP.Controllers
{
    public class AddTestimonialController : Controller
    {
        Models.DoAnOOPEntities2 db = new Models.DoAnOOPEntities2();
        public ActionResult Index()
        {
            var lisofdata = db.Testimonial.ToList();//truy cap va lay tat ca du lieu trong bang NhomHangs
            return View(lisofdata);
        }
        [HttpGet]

        public ActionResult Create() //khi truy cap vao "/Home/Create" thi ham nay se duoc chay va tao 1 form rong de nguoi dung nhap du lieu
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Models.Testimonial model) // yeu cau POST tu form tao moi va them mot bang ghi moi vao NhomHangs
        {
            // Chuyển đổi định dạng ngày từ MM/dd/yyyy sang dd/MM/yyyy


            db.Testimonial.Add(model); // nhan tham so( model) la doi tuong NhomHang chua thong tin nguoi dung nhap vao form va them vao NhomHangs
            db.SaveChanges();//du lieu se duoc luu qua day
            ViewBag.Message = "Đã thêm vào menu";//thong bao cho nguoi dung
            return View();
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Testimonial menu = db.Testimonial.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Testimonial menu = db.Testimonial.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            db.Testimonial.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.Testimonial.Find(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // Thêm phương thức POST để lưu thay đổi sau khi chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Testimonial model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}