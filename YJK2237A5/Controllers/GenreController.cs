using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YJK2237A5.Controllers
{
    public class GenreController : Controller
    {
        private Manager manager = new Manager();
        // GET: Genre
        public ActionResult Index()
        {
            var genres = manager.GenreGetAll();
            return View("Index", genres);
        }
    }
}