using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PagueVeloz.Models
{
    [MetadataType(typeof(EMPRESAMetadata))]
    public partial class EMPRESA
    {
        [Bind(Exclude = "EMP_Id")]
        public class EMPRESAMetadata
        {

            [Display(Name = "Estado")]
            [Required]
            public int UF_Id { get; set; }

            [Display(Name = "Nome Fantasia")]
            [Required]
            public string EMP_NomeFantasia { get; set; }

            [Display(Name = "CNPJ")]
            [Required]
            [MinLength(14)]
            [MaxLength(14)]
            [RegularExpression("([0-9]+)", ErrorMessage = "Favor informar somente números")]
            public string EMP_CNPJ { get; set; }


        }
    }
}
