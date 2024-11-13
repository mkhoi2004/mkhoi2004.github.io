using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OOP.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        Models.DoAnOOPEntities2 db = new Models.DoAnOOPEntities2();

        public ActionResult Index()
        {
            return View();
        }
    }
}