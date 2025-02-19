using Microsoft.EntityFrameworkCore;

public class InventarioContext : DbContext
{
    public InventarioContext(DbContextOptions<InventarioContext> options) : base(options) { }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Transaccion> Transacciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la relación entre Producto y Categoria
        modelBuilder.Entity<Producto>()
            .HasOne<Categoria>()      // No se usa propiedad de navegación en Producto
            .WithMany()               // No se usa propiedad de navegación en Categoria (si la eliminaste)
            .HasForeignKey(p => p.categoriaId)
            .OnDelete(DeleteBehavior.Restrict);  // Evita borrar categorías que se usan en Productos


        // Configuración de relación con Producto, usando solo la clave foránea
        modelBuilder.Entity<Transaccion>()
            .HasOne<Producto>()  // Ya no se usa la propiedad de navegación 'Producto'
            .WithMany()  // Un producto puede tener muchas transacciones
            .HasForeignKey(t => t.productoId)  // La clave foránea en Transaccion
            .OnDelete(DeleteBehavior.Restrict);  // Restrictivo para la eliminación del Producto

        // Configuración de relación con Categoria, usando solo la clave foránea
        modelBuilder.Entity<Transaccion>()
            .HasOne<Categoria>()  // Ya no se usa la propiedad de navegación 'Categoria'
            .WithMany()  // Una categoría puede tener muchas transacciones
            .HasForeignKey(t => t.categoriaId)  // La clave foránea en Transaccion
            .OnDelete(DeleteBehavior.Restrict);  // Restrictivo para la eliminación de la Categoria
    }
}
