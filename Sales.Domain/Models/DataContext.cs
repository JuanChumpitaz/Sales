


namespace Sales.Domain.Models
{
    using Sales.Common.Models;
    using System.Data.Entity;

    public class DataContext : DbContext
    {

        public DataContext() : base("conexion")
        {
        }

        // ESTA LINEA MAPEA LA TABLA Y LA CREA EN LA BD CON EL NOMBRE TBM_PRODU_JCH
        public DbSet<TBM_PRODU_JCH> TBM_PRODU_JCH { get; set; }
    }
}
