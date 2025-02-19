
public interface InterfaceRepository{


    //metodos para productos
    Task<bool> AddProductoRp(Producto nuevoProducto);

    Task<bool> ChangeStockRp(string nombreProducto, string movimiento, int cantidad);


    Task<Producto> GetProductoRp(string nombreProductos);

    Task<IEnumerable<Producto>> GetAllProductoRp();

    Task<int> GetProductoIdRp(string nombreProducto);

    Task<string> GetNombreProductoRp(int productoId);


    Task<bool> SetProductoStatusRp(string nombreProducto, short estatus);


    //metodos para categorias

    Task<bool> AddCategoriaRp(Categoria nuevaCategoria);

    Task<IEnumerable<Categoria>> GetAllCategoriasRp();

    Task<Categoria> GetCategoriaRp(string nombreCategoria);


    Task<string> GetCategoriaNombreRp(int categoriaId);

    public  
    Task<int> GetCategoriaIdRp(string nombreCategoria);

    //metodos para transacciones 
    Task AddTransaccionRp(Transaccion transaccion);  

    Task<IEnumerable<Transaccion>> GetAllTransaccionesRp();

    Task<Transaccion> GetTransaccionRp(int transaccionId);







}