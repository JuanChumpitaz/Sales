using Sales.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales.Backend.Models
{
    public class ProductosJchView : TBM_PRODU_JCH
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}