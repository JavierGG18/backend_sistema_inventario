using Microsoft.AspNetCore.Identity;

public interface InterfaceMapper{


    //metodos para productoDTO
   ProductoDTO ProductoToDTO(Producto producto, string nombreCategoria);

    Producto ProductoToEntity(ProductoDTO productoDTO, int categoriaId);

    //metodos para categorias DTO

    CategoriaDTO CategoriaToDTO(Categoria categoria);

    Categoria CategoriaToEntity(CategoriaDTO categoriaDTO);

    TransaccionDTO TransaccionToDTO(Transaccion transaccion, string nombreCategoria,string nombreProducto);
    Transaccion TransaccionToEntity(TransaccionDTO transaccionDTO, int productoId, int categoriaId);





}