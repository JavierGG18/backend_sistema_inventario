using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


[Index(nameof(nombreCategoria), IsUnique = true)] // Define un índice único en el campo "nombre"
public class Categoria
{
    [Key]
    public int categoriaId { get; set; }

    [Required]
    public string nombreCategoria { get; set; }

    


}
