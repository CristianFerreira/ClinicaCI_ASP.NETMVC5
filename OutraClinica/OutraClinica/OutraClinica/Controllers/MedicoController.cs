using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OutraClinica.Models;
using PagedList;

namespace OutraClinica.Controllers
{
    public class MedicoController : Controller
    {
        //private ClinicaDBContext db = new ClinicaDBContext();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Medico
        public ActionResult Index()
        {
            var medicos = db.Funcionarios.OfType<Medico>();
            return View(medicos.ToList());

        }

        public ActionResult ListaMedicos()
        {
            var medicos = db.Funcionarios.OfType<Medico>();
            return View(medicos.ToList());

        }

        // GET: Medico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Funcionarios.Find(id) as Medico;
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Medico/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Funcionarios.Find(id) as Medico;
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FuncionarioID,Nome,Telefone,rg,endereco,login,senha,especialidades")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaMedicos");
            }
            return View(medico);
        }

        // GET: Medico/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Funcionarios.Find(id) as Medico;
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medico medico = db.Funcionarios.Find(id) as Medico;
            db.Funcionarios.Remove(medico);
            db.SaveChanges();
            return RedirectToAction("ListaMedicos");
        }

        public ActionResult NameFilter(string term)
        {

            term = term.ToLower();
            var filtraName = from nome in db.Funcionarios.OfType<Medico>()
            where (nome.Nome.ToLower().Contains(term))
                             select nome.Nome;
            return Json(filtraName, JsonRequestBehavior.AllowGet);
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
