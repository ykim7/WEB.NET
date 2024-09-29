using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YJK2237A3.Controllers
{
    public class ArtistsController : Controller
    {
        private Manager manager = new Manager();

        // GET: Artist
        public ActionResult Index()
        {
            var artists = manager.ArtistGetAll();
            return View(artists);
        }
    }
}