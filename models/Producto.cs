using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(nombre), IsUnique = true)] // Define un índice único en el campo "nombre"
public class Producto
{
    [Key]
    public int productoId { get; set; }

    [Required]
    public string nombre { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")] // Establecer la precisión y escala
    public decimal precio { get; set; }

    [Required]
    public int stock { get; set; }

    [Required]
    public string marca { get; set; }

    [ForeignKey("Categoria")]
 // Especifica que CategoriaId es la FK
    public int categoriaId { get; set; }

    [Required]
    // 0:fuera de stock , 1:activo 2:descontinuado
    public short status {get; set; }
   
}
