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
    public class ConsultasController : Controller
    {
        private ClinicaDBContext db = new ClinicaDBContext();

        // GET: Consultas
        public ActionResult Index()
        {
            var consulta = db.Consulta.Include(c => c.Paciente).Include(c => c.PlanoDeSaude);
            return View(consulta.ToList());
        }

        // GET: Consultas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consulta.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // GET: Consultas/Create
        public ActionResult Create()
        {
            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "nome");
            ViewBag.PlanoDeSaudeID = new SelectList(db.PlanoDeSaude, "PlanoDeSaudeID", "nome");
            return View();
        }

        // POST: Consultas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsultaID,Data,Hora,PlanoDeSaudeID,PacienteID,MedicoID")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                db.Consulta.Add(consulta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "nome", consulta.PacienteID);
            ViewBag.PlanoDeSaudeID = new SelectList(db.PlanoDeSaude, "PlanoDeSaudeID", "nome", consulta.PlanoDeSaudeID);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consulta.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "nome", consulta.PacienteID);
            ViewBag.PlanoDeSaudeID = new SelectList(db.PlanoDeSaude, "PlanoDeSaudeID", "nome", consulta.PlanoDeSaudeID);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsultaID,Data,Hora,PlanoDeSaudeID,PacienteID,MedicoID")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consulta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "nome", consulta.PacienteID);
            ViewBag.PlanoDeSaudeID = new SelectList(db.PlanoDeSaude, "PlanoDeSaudeID", "nome", consulta.PlanoDeSaudeID);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consulta.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consulta consulta = db.Consulta.Find(id);
            db.Consulta.Remove(consulta);
            db.SaveChanges();
            return RedirectToAction("Index");
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
