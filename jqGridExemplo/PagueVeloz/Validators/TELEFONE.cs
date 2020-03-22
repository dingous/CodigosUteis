using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PagueVeloz.Models
{
    [MetadataType(typeof(TELEFONEMetadata))]
    public partial class TELEFONE
    {
        //[Bind(Exclude = "TEL_Id")]
        public class TELEFONEMetadata
        {

            //[Display(Name = "DDD")]
            //[Required]
            //[MinLength(2)]
            //[MaxLength(2)]
            //[RegularExpression("([0-9]+)", ErrorMessage = "Favor informar somente números")]
            //public int TEL_DDD { get; set; }

            //[Display(Name = "Número do Telefone")]
            //[MinLength(9)]
            //[MaxLength(9)]
            //[RegularExpression("([0-9]+)", ErrorMessage = "Favor informar somente números")]
            //[Required]
            //public int TEL_Numero { get; set; }


        }
    }
}
