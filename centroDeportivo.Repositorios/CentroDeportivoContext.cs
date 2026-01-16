using Microsoft.EntityFrameworkCore; 
using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.Seguridad;

namespace centroDeportivo.Repositorios;

public class CentroDeportivoContext : DbContext
{
    // 1. Las Tablas
    public DbSet<Usuario> Usuarios { get; set; } = null!;
    public DbSet<ActividadDeportiva> Actividades { get; set; } = null!;
    public DbSet<Persona> Personas { get; set; } = null!;

    public DbSet<Estudiante> Estudiante { get; set; } = null!; // Agrega las clases concretas
    public DbSet<Docente> Docente { get; set; } = null!;
    public DbSet<Reserva> Reservas { get; set; } = null!;
    // Agrega aquí el resto de tus entidades...

    public CentroDeportivoContext() {}

    public CentroDeportivoContext(DbContextOptions<CentroDeportivoContext> options) 
        : base(options) { }
    // 2. La Configuración de Conexión
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=centroDeportivo.db");
        }
    }

    // 3. Las Reglas de Mapeo
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Mapeo especial para la lista de permisos (Convertidor de Valor)
        modelBuilder.Entity<Usuario>()
            .Property(u => u.ListaPermisos)
            .HasConversion(
                v => string.Join(',', v ?? new List<Permiso>()), 
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(Enum.Parse<Permiso>)
                      .ToList()
            );
    }
}