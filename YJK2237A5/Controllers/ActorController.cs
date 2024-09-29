using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJK2237A5.Models;
using Microsoft.AspNet.Identity;

namespace YJK2237A5.Controllers
{
    public class ActorController : Controller
    {
        private Manager manager = new Manager();

        // GET: Actor
        public ActionResult Index()
        {
            var actors = manager.ActorGetAll();
            return View("Index", actors);
            
        }

        // GET: Actor/Details/:id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var actor = manager.ActorGetByIdWithDetail(id.Value);

            if (actor == null)
            {
                return HttpNotFound();
            }

            return View(actor);
        }

        // GET: Actor/Add
        [Authorize(Roles = "Executive")]
        public ActionResult ActorAdd()
        {
            var form = new ActorAddFormViewModel();
            form.Executive = User.Identity.GetUserName();
            return View(form);
        }

        //POST: Actor/Add
        [HttpPost]
        [Authorize(Roles = "Executive")]
        [ValidateInput(false)]
        public ActionResult ActorAdd(ActorAddFormViewModel newActor)
        {
            if (ModelState.IsValid)
            {
                var addedActor = manager.ActorAdd(newActor);

                if (addedActor != null)
                {
                    return RedirectToAction("Details", new { id = addedActor.Id });
                }
            }

            return View(newActor);
        }
    }
}