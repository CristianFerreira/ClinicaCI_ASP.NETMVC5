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
    public class PacienteController : Controller
    {
        //private ClinicaDBContext db = new ClinicaDBContext();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paciente
        public ActionResult Index()
        {
            
             return View(db.Paciente.ToList());
        }

        public ActionResult Paciente()
        {
            return View(db.Paciente.ToList());
        }

        // GET: Paciente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
           // paciente.observacoes = paciente.observacoes.Replace("#%#**#**&%%%###", "");
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET: Paciente/Create
        [Authorize(Roles = "Admin, Secretario")]
        public ActionResult Create()
        {

            List<string> estados = new List<string> { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };
            estados.Sort();
            ViewBag.estado = new SelectList(estados);

            return View();
        }

        // POST: Paciente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Secretario")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PacienteID,nome,telefone,DataNascimento,logradouro,numero,bairro,estado")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Paciente.Add(paciente);
                db.SaveChanges();
                return RedirectToAction("Paciente");
            }

            ViewBag.estado = new SelectList(db.Paciente,"estado",paciente.estado);
            return View(paciente);
        }

        [Authorize(Roles = "Medico")]
        public ActionResult EditObs(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
           // paciente.observacoes = paciente.observacoes.Replace("<p>", "");
           // paciente.observacoes = paciente.observacoes.Replace("#%#**#**&%%%###", "");         
            if (paciente == null)
            {
                return HttpNotFound();
            }

            return View(paciente);
        }

        [HttpPost]
        [Authorize(Roles = "Medico")]
        [ValidateAntiForgeryToken]
        public ActionResult EditObs([Bind(Include = "PacienteID,nome,telefone,DataNascimento,logradouro,numero,bairro,estado,observacoes")] Paciente paciente)
        {
            
            if (ModelState.IsValid)
            {
                if (paciente.observacoes==null) {
                    //paciente.observacoes = "<p>"+paciente.observacoes + " - Data e Hora: " + DateTime.Now + "#%#**#**&%%%###";
                    paciente.observacoes = paciente.observacoes;
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                } else { 
                    paciente.observacoes = paciente.observacoes + " - Data e Hora: [ " + DateTime.Now + " ]";
                    db.Entry(paciente).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Paciente");
            }
            return View(paciente);
        }


        // GET: Paciente/Edit/5
        [Authorize(Roles = "Admin, Secretario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            List<string> estados = new List<string> { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };
            estados.Sort();
            ViewBag.estado = new SelectList(estados);


            return View(paciente);
        }

        // POST: Paciente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Secretario")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PacienteID,nome,telefone,DataNascimento,logradouro,numero,bairro,estado,observacoes")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {

                if (paciente.observacoes == null)
                {
                    
                    paciente.observacoes = paciente.observacoes;
                    db.Entry(paciente).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Paciente");
                }
                else
                {
                    paciente.observacoes = paciente.observacoes + " - Data e Hora: [ " + DateTime.Now + " ]";
                    db.Entry(paciente).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Paciente");
                }

                
            }
            ViewBag.estado = new SelectList(db.Paciente, "estado", paciente.estado);
            return View(paciente);
        }

        // GET: Paciente/Delete/5
        [Authorize(Roles = "Admin, Secretario")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin, Secretario")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paciente paciente = db.Paciente.Find(id);
            db.Paciente.Remove(paciente);
            db.SaveChanges();
            return RedirectToAction("Paciente");
        }


        public ActionResult NameFilter(string term)
        {

            term = term.ToLower();
            var filtraName = from nome in db.Paciente
                         where (nome.nome.ToLower().Contains(term))
                         select nome.nome;
            return Json(filtraName, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult RemoveAjax(int id)
        {
            var movie = db.Paciente.Find(id);
            if (movie == null)
                return new HttpStatusCodeResult(
               HttpStatusCode.BadRequest);

            string movieName = movie.nome;
            db.Paciente.Remove(movie);
            db.SaveChanges();
            var results = new
            {
                Message = movieName + "has been removed.",
                DeleteId = id
            };
            return Json(results);

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
