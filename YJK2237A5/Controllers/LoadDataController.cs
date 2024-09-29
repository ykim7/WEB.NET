using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YJK2237A5.Controllers
{
    public class LoadDataController : Controller
    {

        // Reference to the manager object
        Manager m = new Manager();

        // GET: LoadData
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (m.LoadData())
            {
                return Content("Roles data has been loaded");
            }
            else
            {
                return Content("Roles data exists already");
            }
        }

        public ActionResult Remove()
        {
            if (m.RemoveData())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }

        public ActionResult RemoveDatabase()
        {
            if (m.RemoveDatabase())
            {
                return Content("database has been removed");
            }
            else
            {
                return Content("could not remove database");
            }
        }

        [AllowAnonymous]
        public ActionResult Roles()
        {
            if (m.LoadData())
            {
                return Content("Roles data has been loaded");
            }
            else
            {
                return Content("Roles data exists already");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Genres()
        {
            if (m.LoadGenres())
            {
                return Content("Genres data has been loaded");
            }
            else
            {
                return Content("Genres data exists already");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Actors()
        {
            if (m.LoadActors())
            {
                return Content("Actors data has been loaded");
            }
            else
            {
                return Content("Actors data exists already");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Shows()
        {
            if (m.LoadShows())
            {
                return Content("Shows data has been loaded");
            }
            else
            {
                return Content("Shows data exists already");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Episodes()
        {
            if (m.LoadEpisodes())
            {
                return Content("Episodes data has been loaded");
            }
            else
            {
                return Content("Episodes data exists already");
            }
        }


    }
}