namespace SignalRExample2.Infraestructure;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pauta> Pautas { get; set; }
}

public class Pauta
{
    public int Id { get; set; }
    public string Texto { get; set; }
}
