using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJK2237A3.Models;
using System.Diagnostics;

namespace YJK2237A3.Controllers
{
    public class PlaylistsController : Controller
    {
        private Manager manager = new Manager();

        // GET: Playlists
        public ActionResult Index()
        {
            var playlists = manager.PlaylistGetAll();
            return View(playlists);
        }

        // Get: Playlists/Details/:id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var playlist = manager.PlaylistGetById(id.Value);

            if (playlist == null)
            {
                return HttpNotFound();
            }

            return View(playlist);
        }

        // GET: Playlists/Edit/:id
        public ActionResult Edit(int id)
        {
            var formModel = new PlaylistEditTracksFormViewModel();

            var originalModel = manager.PlaylistGetById(id);
            formModel.Name = originalModel.Name;
            formModel.PlaylistId = id;
            formModel.Tracks = originalModel.Tracks;

            formModel.AllTracks = manager.mapper.Map<List<TrackBaseViewModel>>(manager.TrackGetAllWithDetail());
            Debug.WriteLine($"Playlist's enrolled Id is: {formModel.PlaylistId}");

            return View(formModel);
        }

        //POST: Playlists/Edit/:id
        [HttpPost]
        public ActionResult Edit(PlaylistEditTracksFormViewModel editedPlaylist)
        {
            Debug.WriteLine($"Before Playlist's enrolled Id is: {editedPlaylist.PlaylistId}");
            var result = manager.PlaylistEditTracks(editedPlaylist);
            Debug.WriteLine($"After click, result's playlist Id is: {result.PlaylistId}");
            if (result != null)
            {
                return RedirectToAction("Details", new { id = result.PlaylistId });
            }
            
            editedPlaylist.AllTracks = manager.mapper.Map<List<TrackBaseViewModel>>(manager.TrackGetAllWithDetail());
            
            return View(editedPlaylist);
        }
    }
}