public interface InterfaceServices{




     //metodos para productos

    Task<bool> AddProductoSv(ProductoDTO nuevoProducto);

    Task<bool> ChangeStockSv(string nombreProducto, string movimiento, int cantidad);
    Task<bool> SetProductoStatusSv(string nombreProducto, short status);
    Task<ProductoDTO> GetProductoSv(string nombreProducto);

    Task<IEnumerable<ProductoDTO>> GetAllProductoSv();


    //metodos para categorias

    Task<bool> AddCategoriaSv(CategoriaDTO nuevaCategoria);

     Task<IEnumerable<CategoriaDTO>> GetAllCategoriasSv();

    Task<CategoriaDTO> GetCategoriaSv(string nombreCategoria);

    Task<string> GetNombreCategoriaSv(int categoriaId);

    Task<int> GetIdCategoriaSv(string nombreCategoria);

     //metodos para transacciones

    // Task AddTransaccionSv(TransaccionDTO nuevaTransaccion); --metodo privado

    Task<IEnumerable<TransaccionDTO>> GetAllTransaccionesSv();

    Task<TransaccionDTO> GetTransaccionSv(int transaccionId);



    //---------validaciones generales--------
    string ConvertSlugtoNombre(string slug);
    bool ValidateNombreSv(string nombre);

    //--validaciones para productos

    Task<bool> ValidateProductoSv(ProductoDTO producto);
    Task<bool> ValidateStatus(short status, string nombreProducto);






}