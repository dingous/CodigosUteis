using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;

namespace PagueVeloz.Models
{
    [MetadataType(typeof(FORNECEDORMetadata))]
    public partial class FORNECEDOR
    {
        [Bind(Exclude = "FNC_Id")]
        public class FORNECEDORMetadata
        {

            [Display(Name = "Empresa")]
            [Required]
            public int EMP_Id { get; set; }

            [Display(Name = "Nome Fornecedor")]
            [Required]
            public string FND_Nome { get; set; }

            [Display(Name = "CNPJ")]
            [Required]
            [MaxLength(1)]
            public string FND_TipoPessoa { get; set; }


            [Display(Name = "CPF / CNPJ do fornecedor")]
            [Required]
            [MaxLength(14)]
            [RegularExpression("([0-9]+)", ErrorMessage = "Favor informar somente números")]
            public string FND_Documento { get; set; }

            [Display(Name = "RG da Pessoa Física")]
            [RequiredIf("FND_TipoPessoa == 'F'", ErrorMessage = "Para pessoa física necessário informar o RG.")]
            public string FND_RG { get; set; }

            [Display(Name = "Data Nascimento")]
            //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            [RequiredIf("FND_TipoPessoa == 'F'", ErrorMessage = "Para pessoa física necessário informar a Data Nascimento.")]
            public DateTime? FND_DataNascimento { get; set; }
        }
    }
}
