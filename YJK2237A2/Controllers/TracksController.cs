using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJK2237A2.Controllers;
using YJK2237A2.Models;


namespace YJK2237A2.Controllers
{
    public class TracksController : Controller
    {
        private Manager manager = new Manager(); // Create an instance of your Manager class

        // GET: Tracks
        public ActionResult Index()
        {
            var tracks = manager.TrackGetAll();
            return View("Index", tracks); // Use a shared "Index" view for all actions
        }

        public ActionResult BluesJazz()
        {
            var tracks = manager.TrackGetBluesJazz();
            return View("Index", tracks);
        }

        public ActionResult CantrellStaley()
        {
            var tracks = manager.TrackGetCantrellStaley();
            return View("Index", tracks);
        }

        public ActionResult Top50Longest()
        {
            var tracks = manager.TrackGetTop50Longest();
            return View("Index", tracks);
        }

        public ActionResult Top50Smallest()
        {
            var tracks = manager.TrackGetTop50Smallest();
            return View("Index", tracks);
        }

    }
}

