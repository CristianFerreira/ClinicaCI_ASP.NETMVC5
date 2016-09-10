using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OutraClinica.Models
{
    public class PlanoDeSaude
    {
        public int PlanoDeSaudeID { get; set; }

        [Required(ErrorMessage = "Informe nome")]
        [Display(Name = "Nome")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Nome deve ter no minimo 3 caracteres!")]
        public string nome { get; set; }

        public virtual ICollection<Consulta> Consultas { get; set; }
    }
}