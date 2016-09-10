using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OutraClinica.Models
{
    public class Consulta
    {
        public int ConsultaID { get; set; }

        [Required]
        [Display(Name = "Data da Consulta")]
        //[DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Horario da Consulta")]
        //[DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public string Hora { get; set; }

        public bool Compareceu { get; set; }

        [Required]
        [Display(Name = "Plano de Saude")]
        public int PlanoDeSaudeID { get; set; }
        public virtual PlanoDeSaude PlanoDeSaude { get; set; }

        [Required]
        [Display(Name = "Paciente")]
        public int PacienteID { get; set; }
        public virtual Paciente Paciente { get; set; }

        [Required]
        [Display(Name = "Medico")]
        public int MedicoID { get; set; }
        public virtual Medico Medico { get; set; }
    }
}