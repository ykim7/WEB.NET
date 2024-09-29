using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJK2237A5.Models;
using Microsoft.AspNet.Identity;

namespace YJK2237A5.Controllers
{
    public class ShowController : Controller
    {
        private Manager manager = new Manager();

        // GET: Show
        public ActionResult Index()
        {
            var shows = manager.ShowGetAll();
            return View("Index", shows);
        }

        // GET: Shows/Details/:id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var show = manager.ShowGetByIdWithDetail(id.Value);

            if (show == null)
            {
                return HttpNotFound();
            }

            return View(show);
        }


        // GET: Show/Add
        [Authorize(Roles = "Coordinator")]
        public ActionResult ShowAdd(int? preselectedActorId)
        {
            System.Diagnostics.Debug.WriteLine("Inside showAdd");
            var formModel = new ShowAddFormViewModel();

            formModel.Coordinator = User.Identity.GetUserName();
            System.Diagnostics.Debug.WriteLine(User.Identity.GetUserName());
            
            formModel.GenreList = new SelectList(manager.GenreGetAll(), "Name", "Name");
            formModel.Genre = formModel.GenreList.FirstOrDefault()?.Value;

            formModel.ActorList = new MultiSelectList(manager.ActorGetAll(), "Id", "Name");

            if (preselectedActorId.HasValue)
            {
                formModel.PreselectedActor = manager.ActorGetByIdWithDetail(preselectedActorId.Value);
                formModel.SelectedActorIds = new int[] { preselectedActorId.Value };
            }
            return View(formModel);
        }

        //POST: Show/Add
        [HttpPost]
        [Authorize(Roles = "Coordinator")]
        public ActionResult ShowAdd(ShowAddFormViewModel newShow)
        {
            System.Diagnostics.Debug.WriteLine("#######Inside ShowAdd POST");
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("#######Try to Add");
                var addedShow = manager.ShowAdd(newShow);

                if (addedShow != null)
                {
                    return RedirectToAction("Details", new { id = addedShow.Id });
                }

            }

            return View(newShow);
        }
    }
}