public class InventarioMapper : InterfaceMapper{


    //mapeadores para producto
  public ProductoDTO ProductoToDTO(Producto producto, string categoriaNombre)
    {
        return new ProductoDTO
        {
            nombre = producto.nombre,
            marca = producto.marca,
            status = producto.status,
            stock = producto.stock,
            precio = producto.precio,
            categoria = categoriaNombre  // Asignar el nombre de la categoría
        };
    }
    public Producto ProductoToEntity(ProductoDTO productoDTO, int categoriaId){

        return new Producto 
        {
            nombre = productoDTO.nombre,
            marca = productoDTO.marca,
            status = productoDTO.status,
            stock = productoDTO.stock,
            precio = productoDTO.precio,
            categoriaId = categoriaId


        };
    }

    //mapeadores para categoria 
    public CategoriaDTO CategoriaToDTO(Categoria categoria){

        return new CategoriaDTO
        {
            nombreCategoria = categoria.nombreCategoria
         };
    }

    public Categoria CategoriaToEntity(CategoriaDTO categoriaDTO){

        return new Categoria
        {
            nombreCategoria = categoriaDTO.nombreCategoria
        };
    }

    //mapeadores para transacciones
    public TransaccionDTO TransaccionToDTO(Transaccion transaccion, string nombreCategoria,string nombreProducto){

        return new TransaccionDTO
        {
            transaccionId = transaccion.transaccionId,
            fecha = transaccion.fecha,
            accion=transaccion.accion,
            nombreCategoria = nombreCategoria,
            nombreProducto = nombreProducto,
            cantidad = transaccion.cantidad,
            costo = transaccion.costo
        };

    }

    public Transaccion TransaccionToEntity(TransaccionDTO transaccionDTO, int productoId, int categoriaId){

        return new Transaccion
        {
            transaccionId = transaccionDTO.transaccionId,
            fecha = transaccionDTO .fecha,
            accion = transaccionDTO .accion,
            productoId = productoId,
            cantidad = transaccionDTO .cantidad,
            categoriaId = categoriaId,
            costo = transaccionDTO.costo
        };
    }


}