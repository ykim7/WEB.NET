using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YJK2237A1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Do you want to know about Concert Halls?";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Yujin Kim";

            return View();
        }
    }
}