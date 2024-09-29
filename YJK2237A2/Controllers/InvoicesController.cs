using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YJK2237A2.Models;
using System.Web.Mvc;
using YJK2237A2.Controllers;

namespace YJK2237A2.Controllers
{
    public class InvoicesController : Controller
    {
        private Manager manager = new Manager();

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = manager.InvoiceGetAll();
            return View(invoices);
        }

        // GET: Invoices/Detail/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var invoice = manager.InvoiceGetByIdWithDetail(id.Value);

            if (invoice == null)
            {
                return HttpNotFound();
            }

            return View(invoice);
        }

        // You can remove other actions like Edit, Create, Delete, etc. as they're not required in this context.
    }
}