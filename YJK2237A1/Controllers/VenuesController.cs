using System.Collections.Generic;
using System.Web.Mvc;
using YJK2237A1.Models; // Ensure the correct namespace for the view models is used

namespace YJK2237A1.Controllers
{
    public class VenuesController : Controller
    {
        // Reference to the manager object
        private Manager m = new Manager();

        // GET: Venues
        public ActionResult Index()
        {
            // Fetch all venues
            var venues = m.VenueGetAll();
            return View(venues);
        }

        // GET: Venues/Details/5
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                var venue = m.VenueGetById(id.Value);
                if (venue != null)
                {
                    return View(venue);
                }
            }
            return HttpNotFound();
        }

        // GET: Venues/Create
        public ActionResult Create()
        {
            var model = new VenueAddViewModel();
            return View(model);
        }

        // POST: Venues/Create
        [HttpPost]
        public ActionResult Create(VenueAddViewModel newItem)
        {
            if (ModelState.IsValid)
            {
                var addedItem = m.VenueAdd(newItem);

                if (addedItem != null)
                {
                    return RedirectToAction("Details", new { id = addedItem.VenueId });
                }
            }

            return View(newItem);
        }

        // GET: Venues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var venue = m.VenueGetById(id.Value);
                if (venue != null)
                {
                    var editForm = m.mapper.Map<VenueEditFormViewModel>(venue);
                    return View(editForm);
                }
            }
            return HttpNotFound();
        }

        // POST: Venues/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, VenueEditFormViewModel item)
        {
            if (id.HasValue && ModelState.IsValid)
            {
                // Convert VenueEditFormViewModel to VenueEditViewModel
                var editViewModel = m.mapper.Map<VenueEditViewModel>(item);

                var editedItem = m.VenueEdit(editViewModel);
                if (editedItem != null)
                {
                    return RedirectToAction("Details", new { id = editedItem.VenueId });
                }
            }
            return View(item);
        }

        // GET: Venues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                var venue = m.VenueGetById(id.Value);
                if (venue != null)
                {
                    return View(venue);
                }
            }
            return HttpNotFound();
        }

        // POST: Venues/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            if (id.HasValue)
            {
                var result = m.VenueDelete(id.Value);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }
    }
}
