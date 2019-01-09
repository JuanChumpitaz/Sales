

namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TBM_PRODU_JCH
    {
        [Key]
        public int COD_PRODU { get; set; }
        [Required]//-- CAMPO OBLIGATORIO
        public string DES_PRODU       {get;set;}
        public string COD_USUAR_CREAC {get;set;}
        public DateTime FEC_USUAR_CREAC {get;set;}
        public string COD_USUAR_MODIF {get;set;}
        public DateTime FEC_USUAR_MODIF {get;set;}

        public override string ToString()
        {
            return this.DES_PRODU;
        }
    }
}
