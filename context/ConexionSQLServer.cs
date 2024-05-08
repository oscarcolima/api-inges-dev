
using api_inges_dev.Models;
using Microsoft.EntityFrameworkCore;

namespace api_inges_dev.Context
{

    public class ConexionSQLServer : DbContext
    {
        public ConexionSQLServer(DbContextOptions<ConexionSQLServer> options)
            : base(options)
        {
            //
        }

        public DbSet<Technologies> technologies { get; set; }
        public DbSet<Registro> Registers { get; set; }
        


    }
}