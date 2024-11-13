using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOP.Models;
namespace OOP.Controllers
{
    public class TestimonialController : Controller
    {
        // GET: Testimonial
        Models.DoAnOOPEntities2 db = new Models.DoAnOOPEntities2();

        public ActionResult Index()
        {
            return View();
        }
    }
}