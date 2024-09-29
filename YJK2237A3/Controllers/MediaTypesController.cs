using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YJK2237A3.Controllers
{
    public class MediaTypesController : Controller
    {
        private Manager manager = new Manager();

        // GET: MediaTypes
        public ActionResult Index()
        {
            var mediaTypes = manager.MediaTypeGetAll();
            return View(mediaTypes);
        }
    }
}