using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OutraClinica.Models
{
    public class Funcionario
    {
        public int FuncionarioID { get; set; }

        [Required(ErrorMessage = "Informe nome")]
        [Display(Name = "Nome")]
        [StringLength(55, MinimumLength = 3, ErrorMessage = "Nome deve ter no minimo 3 caracteres!")]
        public string Nome { get; set; }

        [Display(Name = "Telefone")]
        [RegularExpression("^\\([1-9]{2}\\) [2-9][0-9]{3}\\-[0-9]{4,5}$", ErrorMessage = "Telefone invalido!")]
        public string Telefone { get; set; }

        [Display(Name = "RG")]
        [RegularExpression("[0-9]{2}\\.[0-9]{3}\\.[0-9]{3}\\-[0-9]{1}$", ErrorMessage = "RG invalido!")]
        
        public string rg { get; set; }

        [Display(Name = "Endereço")]
        public string endereco { get; set; }

        [Required(ErrorMessage = "Informe Email")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Informe um endereço de email valido")]
        [Display(Name = "Email")]
        public string email { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}