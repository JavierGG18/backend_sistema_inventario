using System.Text.RegularExpressions;


public class InventarioService : InterfaceServices{

    private InterfaceRepository _inventarioRepository;
    private InterfaceMapper _mapper;
    public InventarioService(InterfaceRepository inventarioRepository, InterfaceMapper mapper){
        _inventarioRepository = inventarioRepository;
        _mapper = mapper;
    }

//metodos para productos
    public async Task<bool> AddProductoSv(ProductoDTO nuevoProducto){
        //obtenemos el id de la categoria del producto
        var categoriaId = await GetIdCategoriaSv(nuevoProducto.categoria);
        var crearProducto =  await _inventarioRepository.AddProductoRp(_mapper.ProductoToEntity(nuevoProducto, categoriaId));
        if (crearProducto == true){
        //añadir transaccion
            var transaccion = new TransaccionDTO
             {
                fecha = DateTime.Now,
                accion = "NUEVO PRODUCTO",
                nombreProducto = nuevoProducto.nombre,
                nombreCategoria = nuevoProducto.categoria,
                cantidad = nuevoProducto.stock,
                costo =  nuevoProducto.precio * nuevoProducto.stock,
             };
            await AddTransaccionSv(transaccion); 
            return true; 
        }
        return false;
    }

    public async Task<bool> ChangeStockSv(string nombreProducto, string movimiento, int cantidad){
        
        var producto = await GetProductoSv(nombreProducto);
         if (producto !=null){
            var movimientoStock = await _inventarioRepository.ChangeStockRp(nombreProducto, movimiento,cantidad);
            if (movimientoStock){
                //añadir transaccion
                var transaccion = new TransaccionDTO
                {
                    fecha = DateTime.Now,
                    accion = movimiento,
                    nombreProducto = nombreProducto,
                    nombreCategoria = producto.categoria,
                    cantidad = cantidad,
                    costo = producto.precio * cantidad
                };
                    await AddTransaccionSv(transaccion); 
                    return true; 
                }
                 return false;
         }
         return false;

    }

    public async Task<bool> SetProductoStatusSv(string nombreProducto, short status){

        var producto = await GetProductoSv(nombreProducto);
        if (producto != null){
            var changeStatus = await _inventarioRepository.SetProductoStatusRp(nombreProducto, status);
            if (changeStatus){
             //añadir transaccion
                var transaccion = new TransaccionDTO
             {
                fecha = DateTime.Now,
                accion = "CAMBIO DE STATUS",
                nombreProducto = nombreProducto,
                nombreCategoria = producto.categoria,
                cantidad = 0,
                costo = 0
             };
                await AddTransaccionSv(transaccion); 
                return true; 
            }
            return false;
        }
        return false;

    }

    public async Task<ProductoDTO> GetProductoSv(string nombreProducto)
    {
        // Obtener el producto por nombre
        var producto = await _inventarioRepository.GetProductoRp(nombreProducto);
        
        if (producto == null)
        {
            // Manejar el caso en que no se encuentra el producto
            return null;
        }

        // Llamada al servicio para obtener el nombre de la categoría
        string categoriaNombre = await GetNombreCategoriaSv(producto.categoriaId);

        // Usar el mapper para mapear el Producto a ProductoDTO
        var productoDto = _mapper.ProductoToDTO(producto, categoriaNombre);
        return productoDto;
    }
    private async Task<int> GetProductoIdSv(string nombreProducto){
        return await _inventarioRepository.GetProductoIdRp(nombreProducto);
    }
    private async Task<string> GetNombreProductoSv(int productoId){
        return await _inventarioRepository.GetNombreProductoRp(productoId);
    }

    public async Task<IEnumerable<ProductoDTO>> GetAllProductoSv()
    {
    var registros = await _inventarioRepository.GetAllProductoRp();
    var productosDTO = new List<ProductoDTO>();

    foreach (var p in registros)
    {
        var nombreCategoria = await GetNombreCategoriaSv(p.categoriaId);
        productosDTO.Add(_mapper.ProductoToDTO(p, nombreCategoria));
    }

    return productosDTO;
    }

   


    //metodos categorias
    public async Task<bool> AddCategoriaSv(CategoriaDTO nuevaCategoria){
        
        return await _inventarioRepository.AddCategoriaRp(_mapper.CategoriaToEntity(nuevaCategoria));
    }

    public async Task<IEnumerable<CategoriaDTO>> GetAllCategoriasSv(){
        var categorias=  await _inventarioRepository.GetAllCategoriasRp();

        var categoriasDTo =  categorias.Select(c => _mapper.CategoriaToDTO(c));

        return categoriasDTo;
    }

    public async Task<CategoriaDTO> GetCategoriaSv(string nombreCategoria){

        var categoria = await _inventarioRepository.GetCategoriaRp(nombreCategoria);
        if (categoria == null){
            return null;
        }
        var CategoriaDTO = _mapper.CategoriaToDTO(categoria);
        return CategoriaDTO;
    }

    public async Task<string> GetNombreCategoriaSv(int categoriaId){

        var nombreCategoria = await _inventarioRepository.GetCategoriaNombreRp(categoriaId);
        return  nombreCategoria;
    }

    public async Task<int> GetIdCategoriaSv(string nombreCategoria){

        var categoriaId = await _inventarioRepository.GetCategoriaIdRp(nombreCategoria);
        return categoriaId;

    }


    //metodos para transacciones


    private async Task AddTransaccionSv(TransaccionDTO nuevaTransaccion){

        var categoriaId = await GetIdCategoriaSv(nuevaTransaccion.nombreCategoria);
        var productoId = await GetProductoIdSv(nuevaTransaccion.nombreProducto);
        await _inventarioRepository.AddTransaccionRp(_mapper.TransaccionToEntity(nuevaTransaccion,productoId,categoriaId));
    }

    public async Task<IEnumerable<TransaccionDTO>> GetAllTransaccionesSv(){
        var registros = await _inventarioRepository.GetAllTransaccionesRp();
        var transaccionesDTO = new List<TransaccionDTO>();

        foreach (var t in registros)
        {
            var nombreCategoria = await GetNombreCategoriaSv(t.categoriaId);
            var nombreProducto = await GetNombreProductoSv(t.productoId);
            transaccionesDTO.Add(_mapper.TransaccionToDTO(t, nombreCategoria, nombreProducto));
        }

        return transaccionesDTO;
    }


    public async Task<TransaccionDTO> GetTransaccionSv(int transaccionId){

        var transaccion = await _inventarioRepository.GetTransaccionRp(transaccionId);
        var nombreCategoria = await GetNombreCategoriaSv(transaccion.categoriaId);
        var nombreProducto = await GetNombreProductoSv(transaccion.productoId);
        return _mapper.TransaccionToDTO(transaccion, nombreCategoria,nombreProducto);
    }






    //-------validaciones generales-------
    public string ConvertSlugtoNombre(string slug)  
    {
    string patron = @"^[a-z0-9]+(-[a-z0-9]+)*$";
    Regex validadorSlug = new Regex(patron);

    if (validadorSlug.IsMatch(slug))
    {
        // Reemplazamos los guiones por espacios y convertimos a mayúsculas
        string nombre = slug.Replace("-", " ").ToUpper();
        return nombre;
    }
    return string.Empty;
    }

    public bool ValidateNombreSv(string nombre)
    {
    string patron = @"^[A-Z0-9]+( [A-Z0-9]+)*$"; 
    Regex validadorCadena = new Regex(patron);
    return validadorCadena.IsMatch(nombre);
    }

    //--------validaciones para productos
    public bool ValidateProductoSv(ProductoDTO producto)
    {
    if (producto == null)
        return false;

    string patron = @"^[A-Z0-9]+( [A-Z0-9]+)*$";
    Regex validadorCadena = new Regex(patron);



    return validadorCadena.IsMatch(producto.nombre) &&
           validadorCadena.IsMatch(producto.marca) &&
           validadorCadena.IsMatch(producto.categoria) &&
           producto.precio > 0 &&
           producto.stock > 0 &&
           producto.status == 1 ;
    }
    
    public async Task<bool> ValidateStatus(short status, string nombreProducto)
    {
    var producto = await GetProductoSv(nombreProducto);
    if (producto == null)
    {
        return false;
    }

    return status switch
    {
        // Descontinuar
        0 => producto.stock == 0,

        // Activar
        1 => producto.stock > 0,

        // Fuera de stock
        2 => producto.stock == 0,

        // Estado inválido
        _ => false
    };  
    }



}





