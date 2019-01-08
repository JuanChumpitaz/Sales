using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Models
{
    public class DataContext : DbContext
    {

        public DataContext() : base("conexion")
        {
        }

        public System.Data.Entity.DbSet<Sales.Common.Models.TBM_PRODU_JCH> TBM_PRODU_JCH { get; set; }
    }
}
