using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using OOP.Models;

namespace OOP.Controllers
{
    public class HomeController : Controller
    {
        Models.DoAnOOPEntities2 db = new Models.DoAnOOPEntities2();

        public ActionResult Index()
        {
            // Lấy tất cả các phiếu từ cơ sở dữ liệu
            var listOfData = db.Reservation.ToList();

            // Nếu không có phiếu nào trong cơ sở dữ liệu, gán SoPhieu của phiếu mới là 1
            string nextSoPhieu = "1";

            // Nếu có phiếu trong cơ sở dữ liệu, tìm SoPhieu lớn nhất và tăng lên 1
            if (listOfData.Any())
            {
                var maxSoPhieu = listOfData.Max(p => Convert.ToInt32(p.reservation_id));
                nextSoPhieu = (maxSoPhieu + 1).ToString();
            }

            // Tạo một phiếu mới với giá trị SoPhieu đã được tăng
            var newPhieu = new Reservation { reservation_id = nextSoPhieu };
            return View(newPhieu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index(Models.Reservation model) // yeu cau POST tu form tao moi va them mot bang ghi moi vao NhomHangs
        {


            db.Reservation.Add(model); // nhan tham so( model) la doi tuong NhomHang chua thong tin nguoi dung nhap vao form va them vao NhomHangs
            db.SaveChanges();//du lieu se duoc luu qua day
            ViewBag.Message = "Đã thêm vào menu";//thong bao cho nguoi dung
            return View();
        }

    }
}