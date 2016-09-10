using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OutraClinica.Models
{
    public class Paciente
    {
        public int PacienteID { get; set; }


        [Required]
        [Display(Name = "Nome")]
        [StringLength(55, MinimumLength = 3, ErrorMessage = "Nome deve ter no minimo 3 caracteres!")]
        public string nome { get; set; }

        [Required]
        [Display(Name = "Telefone")]
        [RegularExpression("^\\([1-9]{2}\\) [2-9][0-9]{3}\\-[0-9]{4,5}$", ErrorMessage = "Telefone invalido!")]
        public string telefone { get; set; }

        [Display(Name = "Data Nascimento")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public string DataNascimento { get; set; }

        [Display(Name = "Logradouro")]
        public string logradouro { get; set; }
        [Display(Name = "N°")]
        public string numero { get; set; }
        [Display(Name = "Bairro")]
        public string bairro { get; set; }
        [Display(Name = "Estado")]
        public string estado { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Observações")]
        public string observacoes { get; set; }

        public virtual ICollection<Consulta> Consultas { get; set; }
    }
}