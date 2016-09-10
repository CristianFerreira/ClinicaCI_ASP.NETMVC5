using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OutraClinica.Models
{
    public class Medico : Funcionario
    {
        [DataType(DataType.MultilineText)]
        [Display(Name = "Especialidades")]
        public string especialidades { get; set; }

        public virtual ICollection<Consulta> Consultas { get; set; }
    }
}