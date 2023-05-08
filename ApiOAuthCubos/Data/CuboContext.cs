using ApiOAuthCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthCubos.Data
{
    public class CuboContext : DbContext
    {
        public CuboContext(DbContextOptions<CuboContext> options)
           : base(options) { }
        public DbSet<CompraCubos> CompraCubos { get; set; }
        public DbSet<Cubos> Cubos { get; set; }
        public DbSet<UsuariosCubo> UsuarioCubo { get; set; }

    }
}
