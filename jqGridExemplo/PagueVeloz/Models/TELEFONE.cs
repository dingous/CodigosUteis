//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PagueVeloz.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TELEFONE
    {
        public int TEL_Id { get; set; }
        public int TEL_DDD { get; set; }
        public int TEL_Numero { get; set; }
        public int FND_Id { get; set; }
    
        public virtual FORNECEDOR FORNECEDOR { get; set; }
    }
}
