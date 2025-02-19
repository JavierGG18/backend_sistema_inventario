using Microsoft.EntityFrameworkCore;

public class InventarioRepository : InterfaceRepository{

private InventarioContext _context;
public InventarioRepository(InventarioContext context){
    _context = context;
}

    //Metodos para productos
    public async Task<bool> AddProductoRp(Producto nuevoProducto)
    {
        //verificamos si ya existe el producto que queremos agregar
        var producto = await _context.Productos.FirstOrDefaultAsync(p => p.nombre == nuevoProducto.nombre);
        if (producto == null){

            await _context.Productos.AddAsync(nuevoProducto);
            await _context.SaveChangesAsync();
            return true;  // Éxito
        }
        return false;

    }

    public async Task<bool> ChangeStockRp(string nombreProducto, string movimiento, int cantidad)
    {
        // Verificamos si el producto existe
        var producto = await GetProductoRp(nombreProducto);
         if (producto == null)
            {
                return false;
            }

        // Evaluar el movimiento proporcionado
        switch (movimiento)
        {
            case "ENTRADA":
             producto.stock = producto.stock + cantidad;
                break;

            case "SALIDA":
                if (cantidad > producto.stock)
                {
                    return false;
                }
                producto.stock = producto.stock - cantidad;
                //verificamos si el producto esta fuera de stock para actualizar su status
                if (producto.stock == 0)
                {
                    producto.status = 0; // ponemos el producto fuera de stock
                }
                break;

            default:
                return false;
        }

        // Guardar los cambios en la base de datos
        await _context.SaveChangesAsync();
        return true;
    }



    public async Task<bool> SetProductoStatusRp(string nombreProducto, short status)
    {
    // Verificamos si el producto existe
    var producto = await GetProductoRp(nombreProducto);

    if (producto == null)
    {
        return false;
    }

    // Verificamos si ya tiene el estado deseado
    if (producto.status == status)
    {
        return false; // No se realiza ningún cambio
    }

    // Cambiamos el estado y guardamos
    producto.status = status;
    await _context.SaveChangesAsync();
    return true;    
    }

     public async Task<Producto> GetProductoRp(string nombreProducto){

      return await _context.Productos.FirstOrDefaultAsync(p => p.nombre == nombreProducto);

     }

    public async Task<IEnumerable<Producto>> GetAllProductoRp(){

        return await _context.Productos.ToListAsync();
    }

    public async Task<int> GetProductoIdRp(string nombreProducto)
    {
    var producto = await _context.Productos.FirstOrDefaultAsync(p => p.nombre == nombreProducto);

    if(producto != null){
    return producto.productoId;
    }

    return 0; // Retornar 0 si no se encuentra la categoría
    }
    
    public async Task<string> GetNombreProductoRp(int productoId){
        var producto = await _context.Productos.FirstOrDefaultAsync(p => p.productoId == productoId);
        if (producto != null){
            return producto.nombre;
        }
            return string.Empty; //retornar vacio si no se encuentra la categoria

        }
    
    
    //metodos para categoria

    public async Task<bool> AddCategoriaRp(Categoria nuevaCategoria){
        var Exist = await _context.Categorias.FirstOrDefaultAsync(c => c.nombreCategoria == nuevaCategoria.nombreCategoria);
        if (Exist == null){
            await _context.Categorias.AddAsync(nuevaCategoria);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<Categoria>> GetAllCategoriasRp(){

        return await _context.Categorias.ToListAsync();
    }
    
    public async Task<Categoria> GetCategoriaRp(string nombreCategoria){
        return await _context.Categorias.FirstOrDefaultAsync(c => c.nombreCategoria == nombreCategoria);
        
    }

    public async Task<string> GetCategoriaNombreRp(int categoriaId)
    {
    var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.categoriaId == categoriaId);
    if (categoria != null){

        return categoria.nombreCategoria;
    }

    return string.Empty; //retornar vacio si no se encuentra la categoria
    }

   public async Task<int> GetCategoriaIdRp(string nombreCategoria)
    {
    var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.nombreCategoria == nombreCategoria);

    if(categoria != null){
    return categoria.categoriaId;
    }

    return 0; // Retornar 0 si no se encuentra la categoría
    }


    //metodos para transacciones

    public async Task AddTransaccionRp(Transaccion transaccion){
        await _context.Transacciones.AddAsync(transaccion);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Transaccion>> GetAllTransaccionesRp(){
        return await _context.Transacciones.ToListAsync();
    }

    public async Task<Transaccion> GetTransaccionRp(int transaccionId){
        return await _context.Transacciones.FindAsync(transaccionId);

    }




}





