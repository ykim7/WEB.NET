using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YJK2237A3.Controllers
{
    public class AlbumsController : Controller
    {
        private Manager manager = new Manager();

        // GET: Albums
        public ActionResult Index()
        {
            var albums = manager.AlbumGetAll();
            return View(albums);
        }
    }
}