using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Transaccion{

    [Key]
    public int transaccionId { get; set; }
    [Required]
    public DateTime fecha { get; set; }
    [Required]
    public string accion { get; set; } //ENTRADA - //SALIDA  - //CAMBIO DE ESTATUS

    [ForeignKey("Producto")]
    public int productoId { get; set; }

    [Required]
    public int cantidad { get; set; } //referencia a la cantidad que entra o sale del stock

    [ForeignKey("Categoria")] // Especifica que CategoriaId es la FK
    public int categoriaId { get; set; }

    [Column(TypeName = "decimal(18,2)")] // Establecer la precisión y escala
    [Required]
    public decimal costo { get; set;}

  
}