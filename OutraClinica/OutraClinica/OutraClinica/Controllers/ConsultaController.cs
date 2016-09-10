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
    public class ConsultaController : Controller
    {
        //private ClinicaDBContext db = new ClinicaDBContext();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Consulta
        public ActionResult Index()
        {


            var consulta = db.Consulta.Include(c => c.Paciente).Include(c => c.PlanoDeSaude).Include(c => c.Medico);

            List<Consulta> listaConsulta = new List<Consulta>();


            //consulta = consulta.Where(c => c.Data >= DateTime.Now).OrderBy(c => c.Data);

            foreach (var data in consulta)
            {
                DateTime dataConsulta = data.Data;
                string aux = dataConsulta.ToShortDateString() + " " + data.Hora;
                dataConsulta = Convert.ToDateTime(aux);

                if (dataConsulta >= DateTime.Now)
                {
                    listaConsulta.Add(data);

                }



            }

            return View(listaConsulta);



            //  var dataAtual = db.Consulta.Where(c => c.Data >= DateTime.Now);

            // var consulta = db.Consulta.Include(c => c.Paciente).Include(c => c.PlanoDeSaude).Include(c => c.Medico);


            //return View(dataAtual.ToList());
        }

        public ActionResult ConsultasEmAndamento()
        {

            var userName = User.Identity.Name;
            var usuario = db.Users.Where(x => x.UserName == userName).FirstOrDefault();




            Boolean secretaria = usuario.Roles.Any(c => c.RoleId == "2");
            Boolean medico = usuario.Roles.Any(c => c.RoleId == "3");
            Boolean adm = usuario.Roles.Any(c => c.RoleId == "1");


            var consulta = db.Consulta.Include(c => c.Paciente).Include(c => c.PlanoDeSaude).Include(c => c.Medico);

            List<Consulta> listaConsulta = new List<Consulta>();


            //consulta = consulta.Where(c => c.Data >= DateTime.Now).OrderBy(c => c.Data);

            foreach (var data in consulta)
            {
                DateTime dataConsulta = data.Data;
                string aux = dataConsulta.ToShortDateString() + " " + data.Hora;
                dataConsulta = Convert.ToDateTime(aux);



                if (!adm && !secretaria)
                {
                    if (usuario.UserName == data.Medico.email && dataConsulta >= DateTime.Now)
                    {
                        listaConsulta.Add(data);
                    }
                }
                else
                {
                    if (adm && dataConsulta >= DateTime.Now)
                    {
                        listaConsulta.Add(data);
                    }else if (secretaria && dataConsulta >= DateTime.Now)
                    {
                        listaConsulta.Add(data);
                    }
                }



            }

            return View(listaConsulta);
        }


        public ActionResult ProximasConsultas()
        {

            var userName = User.Identity.Name;
            var usuario = db.Users.Where(x => x.UserName == userName).FirstOrDefault();

            
            Boolean medico = usuario.Roles.Any(c => c.RoleId == "3");
            Boolean adm = usuario.Roles.Any(c => c.RoleId == "1");

            var consulta = db.Consulta.Include(c => c.Paciente).Include(c => c.PlanoDeSaude).Include(c => c.Medico);


            List<Consulta> recebe = new List<Consulta>();




            //consulta = consulta.Where(c => c.Data >= DateTime.Now).OrderBy(c => c.Data);

            foreach (var data in consulta)
            {
                DateTime dataConsulta = data.Data;
                string aux = dataConsulta.ToShortDateString() + " " + data.Hora;
                dataConsulta = Convert.ToDateTime(aux);

                if (!adm)
                {
                    if (usuario.UserName == data.Medico.email && dataConsulta > DateTime.Now.AddHours(0) && dataConsulta < DateTime.Now.AddHours(24))
                    {
                        recebe.Add(data);
                    }
                }
                else
                {
                    if (adm && dataConsulta > DateTime.Now.AddHours(0) && dataConsulta < DateTime.Now.AddHours(24))
                    {
                        recebe.Add(data);
                    }
                }



            }

            return View(recebe);



        }

        public ActionResult ConsultasPassadas()
        {
            //var consultas = db.Consulta.Include(c => c.Paciente).Include(c => c.PlanoDeSaude).Include(c => c.Medico);
            //consultas = consultas.Where(c => c.Data < DateTime.Now).OrderBy(c => c.Data);

            // return View(consultas.ToList());


            var userName = User.Identity.Name;
            var usuario = db.Users.Where(x => x.UserName == userName).FirstOrDefault();

            Boolean secretaria = usuario.Roles.Any(c => c.RoleId == "2");
            Boolean medico = usuario.Roles.Any(c => c.RoleId == "3");
            Boolean adm = usuario.Roles.Any(c => c.RoleId == "1");

            var consulta = db.Consulta.Include(c => c.Paciente).Include(c => c.PlanoDeSaude).Include(c => c.Medico);

            List<Consulta> listaConsultasPassadas = new List<Consulta>();


            //consulta = consulta.Where(c => c.Data >= DateTime.Now).OrderBy(c => c.Data);

            foreach (var data in consulta)
            {
                DateTime dataConsulta = data.Data;
                string aux = dataConsulta.ToShortDateString() + " " + data.Hora;
                dataConsulta = Convert.ToDateTime(aux);


                if (!adm && !secretaria)
                {
                    if (usuario.UserName == data.Medico.email && dataConsulta < DateTime.Now || usuario.UserName == data.Medico.email && data.Compareceu == true)
                    {
                        listaConsultasPassadas.Add(data);
                    }
                }
                else
                {
                    if (adm && dataConsulta < DateTime.Now || data.Compareceu == true)
                    {
                        listaConsultasPassadas.Add(data);
                    }
                    else if (secretaria && dataConsulta < DateTime.Now || data.Compareceu == true)
                    {
                        listaConsultasPassadas.Add(data);
                    }
                }



            }

            return View(listaConsultasPassadas);

        }

        public ActionResult ConsultasMedico(int? id)
        {
            var consultas = db.Consulta.Include(c => c.Paciente).Include(c => c.PlanoDeSaude).Include(c => c.Medico);
            consultas = consultas.Where(c => c.Data >= DateTime.Now && c.MedicoID == id);
            //consultas = consultas.OrderBy(c => c.Data).ThenBy(c => c.Hora);

            return View(consultas.ToList());
        }

        [Authorize(Roles = "Medico")]
        public ActionResult MudaCompareceu(int? id)
        {
            var consulta = db.Consulta.Find(id);
            consulta.Compareceu = !(consulta.Compareceu);

            db.Entry(consulta).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ProximasConsultas");
        }

        // GET: Consulta/Details/5
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

        // GET: Consulta/Create
        [Authorize(Roles = "Admin, Secretario")]
        public ActionResult Create()
        {
            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "nome");
            ViewBag.PlanoDeSaudeID = new SelectList(db.PlanoDeSaude, "PlanoDeSaudeID", "nome");
            ViewBag.MedicoID = new SelectList(db.Funcionarios.OfType<Medico>(), "FuncionarioID", "Nome");
            return View();
        }

        // POST: Consulta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Secretario")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsultaID,Data,Hora,PlanoDeSaudeID,PacienteID,MedicoID,Compareceu")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                Medico medico = db.Funcionarios.Find(consulta.MedicoID) as Medico;
                consulta.Medico = medico;
                db.Consulta.Add(consulta);
                db.SaveChanges();
                return RedirectToAction("ConsultasEmAndamento");
            }

            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "nome", consulta.PacienteID);
            ViewBag.PlanoDeSaudeID = new SelectList(db.PlanoDeSaude, "PlanoDeSaudeID", "nome", consulta.PlanoDeSaudeID);
            ViewBag.MedicoID = new SelectList(db.Funcionarios.OfType<Medico>(), "FuncionarioID", "Nome", consulta.MedicoID);
            return View(consulta);
        }

        // GET: Consulta/Edit/5
        [Authorize(Roles = "Admin, Secretario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                throw new HttpException(500, "Id is required.");
            }
            Consulta consulta = db.Consulta.Find(id);
            if (consulta == null)
            {
                //return HttpNotFound();
                throw new HttpException(404, "Invalid Id");
            }
            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "nome", consulta.PacienteID);
            ViewBag.PlanoDeSaudeID = new SelectList(db.PlanoDeSaude, "PlanoDeSaudeID", "nome", consulta.PlanoDeSaudeID);
            ViewBag.MedicoID = new SelectList(db.Funcionarios.OfType<Medico>(), "FuncionarioID", "Nome", consulta.MedicoID);
            return View(consulta);
        }

        // POST: Consulta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Secretario")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsultaID,Data,Hora,PlanoDeSaudeID,PacienteID,MedicoID,Compareceu")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consulta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ConsultasEmAndamento");
            }
            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "nome", consulta.PacienteID);
            ViewBag.PlanoDeSaudeID = new SelectList(db.PlanoDeSaude, "PlanoDeSaudeID", "nome", consulta.PlanoDeSaudeID);
            ViewBag.MedicoID = new SelectList(db.Funcionarios.OfType<Medico>(), "FuncionarioID", "Nome", consulta.MedicoID);
            return View(consulta);
        }

        // GET: Consulta/Delete/5
        [Authorize(Roles = "Admin, Secretario")]
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

            DateTime dataConsulta = consulta.Data;
            string aux = dataConsulta.ToShortDateString() + " " + consulta.Hora;
            dataConsulta = Convert.ToDateTime(aux);

            ViewBag.ValidaConsulta = dataConsulta;

            return View(consulta);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin, Secretario")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consulta consulta = db.Consulta.Find(id);
            db.Consulta.Remove(consulta);
            db.SaveChanges();


            return RedirectToAction("ConsultasEmAndamento");
        }




        [Authorize(Roles = "Admin, Secretario")]
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consulta.Find(id);
            if (consulta == null)
            {
                return RedirectToAction("consultasPassadas");
            }



            return View(consulta);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Excluir")]
        [Authorize(Roles = "Admin, Secretario")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConsultaPassada(int id)
        {
            Consulta consulta = db.Consulta.Find(id);
            db.Consulta.Remove(consulta);
            db.SaveChanges();


            return RedirectToAction("consultasPassadas");
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
