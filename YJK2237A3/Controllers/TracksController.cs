using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJK2237A3.Data;
using YJK2237A3.Models;

namespace YJK2237A3.Controllers
{
    public class TracksController : Controller
    {
        private Manager manager = new Manager();

        // GET: Tracks
        public ActionResult Index()
        {
            var tracks = manager.TrackGetAllWithDetail();
            return View(tracks);
        }

        // Get: Tracks/Details/:id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var track = manager.TrackGetByIdWithDetail(id.Value);

            if (track == null)
            {
                return HttpNotFound();
            }

            return View(track);
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            var formModel = new TrackAddFormViewModel();

            formModel.AlbumSelectList = new SelectList(manager.AlbumGetAll(), "AlbumId", "Title");
            formModel.MediaTypeSelectList = new SelectList(manager.MediaTypeGetAll(), "MediaTypeId", "Name");

            formModel.SelectedAlbumId = Convert.ToInt32(formModel.AlbumSelectList.FirstOrDefault()?.Value);
            formModel.SelectedMediaTypeId = Convert.ToInt32(formModel.MediaTypeSelectList.Skip(1).FirstOrDefault()?.Value);

            return View(formModel);
        }

        //POST: Tracks/Create
        [HttpPost]
        public ActionResult Create(TrackAddFormViewModel newTrack)
        {
            if (ModelState.IsValid)
            {
                var addedTrack = manager.TrackAdd(newTrack);

                if (addedTrack != null)
                {
                    return RedirectToAction("Details", new { id = addedTrack.TrackId });
                }
            }

            return View(newTrack);
        }
    }
    
}