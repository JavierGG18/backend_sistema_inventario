public class TransaccionDTO{

    public int transaccionId { get; set; }

    public DateTime fecha { get; set; }

    public string accion { get; set; }  //ENTRADA - //SALIDA  - //CAMBIO DE ESTATUS -//NUEVO PRODUCTO -//DESCONTINUAR PRODUCTO

    public string nombreProducto { get; set; }

    public string nombreCategoria { get; set; }

    public int cantidad { get; set; }

    public decimal costo { get; set; }
}