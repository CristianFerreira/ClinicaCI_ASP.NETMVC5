using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OutraClinica.Models;

namespace OutraClinica.Controllers
{
    public class PlanoDeSaudeController : Controller
    {
        //private ClinicaDBContext db = new ClinicaDBContext();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlanoDeSaude
        public ActionResult Index()
        {
            return View(db.PlanoDeSaude.ToList());
        }

        public ActionResult PlanoDeSaude()
        {
            return View(db.PlanoDeSaude.ToList());
        }

        // GET: PlanoDeSaude/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoDeSaude planoDeSaude = db.PlanoDeSaude.Find(id);
            if (planoDeSaude == null)
            {
                return HttpNotFound();
            }
            return View(planoDeSaude);
        }

        // GET: PlanoDeSaude/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlanoDeSaude/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlanoDeSaudeID,nome")] PlanoDeSaude planoDeSaude)
        {
            if (ModelState.IsValid)
            {
                db.PlanoDeSaude.Add(planoDeSaude);
                db.SaveChanges();
                return RedirectToAction("PlanoDeSaude");
            }

            return View(planoDeSaude);
        }

        // GET: PlanoDeSaude/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoDeSaude planoDeSaude = db.PlanoDeSaude.Find(id);
            if (planoDeSaude == null)
            {
                return HttpNotFound();
            }
            return View(planoDeSaude);
        }

        // POST: PlanoDeSaude/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlanoDeSaudeID,nome")] PlanoDeSaude planoDeSaude)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planoDeSaude).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PlanoDeSaude");
            }
            return View(planoDeSaude);
        }

        // GET: PlanoDeSaude/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoDeSaude planoDeSaude = db.PlanoDeSaude.Find(id);
            if (planoDeSaude == null)
            {
                return HttpNotFound();
            }
            return View(planoDeSaude);
        }

        // POST: PlanoDeSaude/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanoDeSaude planoDeSaude = db.PlanoDeSaude.Find(id);
            db.PlanoDeSaude.Remove(planoDeSaude);
            db.SaveChanges();
            return RedirectToAction("PlanoDeSaude");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
