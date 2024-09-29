using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJK2237A5.Models;
using Microsoft.AspNet.Identity;

namespace YJK2237A5.Controllers
{
    public class EpisodeController : Controller
    {
        private Manager manager = new Manager();

        // GET: Episode
        public ActionResult Index()
        {
            var episodes = manager.EpisodeGetAll();
            return View("Index", episodes);
        }

        // GET: Episodes/Details/:id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var episode = manager.EpisodeGetByIdWithDetail(id.Value);

            if (episode == null)
            {
                return HttpNotFound();
            }

            return View(episode);
        }

        // GET: Episode/Add
        [Authorize(Roles = "Clerk")]
        public ActionResult EpisodeAdd(int? id)
        {
            var formModel = new EpisodeAddFormViewModel();
            if (id.HasValue)
            {
                formModel.Show = manager.ShowGetById(id.Value);
                System.Diagnostics.Debug.WriteLine(formModel.Show.Name);
                formModel.GenreList = new SelectList(manager.GenreGetAll(), "Name", "Name");
                formModel.Genre = formModel.GenreList.FirstOrDefault()?.Value;
                formModel.Clerk = User.Identity.GetUserName();
                return View(formModel);
            }
            return HttpNotFound();
        }

        // POST: Episode/Add
        [HttpPost]
        [Authorize(Roles = "Clerk")]
        public ActionResult EpisodeAdd(EpisodeAddFormViewModel newEpisode)
        {
            System.Diagnostics.Debug.WriteLine("Inside Episode Add POST");
            System.Diagnostics.Debug.WriteLine(newEpisode.Show);
            System.Diagnostics.Debug.WriteLine(newEpisode.Show.Name);
            System.Diagnostics.Debug.WriteLine(newEpisode.Name);
            System.Diagnostics.Debug.WriteLine(newEpisode.Genre);
            try
            {
                System.Diagnostics.Debug.WriteLine("#####Is valid?");
                if (!ModelState.IsValid)
                {
                    return View(newEpisode);
                }
                System.Diagnostics.Debug.WriteLine("#####It's valid!");

                // TODO: Add insert logic here
                var addedEpisode = manager.EpisodeAdd(newEpisode);
                System.Diagnostics.Debug.WriteLine("Try to submit new episode to database");
                System.Diagnostics.Debug.WriteLine(addedEpisode.Show.Name);
                System.Diagnostics.Debug.WriteLine(addedEpisode.Name);
                if (addedEpisode == null)
                {
                    return View(newEpisode);
                }
                else
                {
                    return RedirectToAction("details", new { id = addedEpisode.Id });
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Route("Episodes/Video/{id}")]
        public ActionResult Video(int id)
        {
            var videoViewModel = manager.EpisodeVideoGetById(id);

            if (videoViewModel != null)
            {
  
                
            }

            return HttpNotFound();
        }
    }
}