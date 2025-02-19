using Microsoft.AspNetCore.Mvc;

public class InventarioController : ControllerBase{

    private InterfaceServices _inventarioService;

    public InventarioController(InterfaceServices inventarioInterface){
        _inventarioService = inventarioInterface;
    }



    //----------endpoints para productos-------------

    //listar todos los productos
    [HttpGet("productos")]
    public async Task <ActionResult<IEnumerable<ProductoDTO>>> GetProductos(){

        var products = await _inventarioService.GetAllProductoSv();
        return Ok(products);

    }
    
    //obtener la info de un producto mediante el nombre
    [HttpGet("productos/{slug}")]
    public async Task <ActionResult<ProductoDTO>> GetProducto (string slug){
        string nombreProducto = _inventarioService.ConvertSlugtoNombre(slug);
        Console.WriteLine(nombreProducto);
        if(nombreProducto == string.Empty){
            return NotFound("no existe el producto ");
        }
        var producto = await _inventarioService.GetProductoSv(nombreProducto);
        if (producto != null){
            return Ok(producto);
        }
        return NotFound("no existe el producto");

    }

    //añadir un producto
    [HttpPost("productos")]
    public async Task<IActionResult> AddProduct([FromBody]ProductoDTO nuevoProducto)
    {
    // Validar los datos del producto
    var validarProducto = _inventarioService.ValidateProductoSv(nuevoProducto);
    if (validarProducto == false)
    {
        return BadRequest("Los datos ingresados para el nuevo producto son inválidos");
    }

    // Verificar si el producto ya existe
    var exists = await _inventarioService.GetProductoSv(nuevoProducto.nombre);
    if (exists != null)  // Verificar si el producto ya existe
    {
        return BadRequest("El producto ya existe en los registros");
    }

    // Agregar el nuevo producto
    var operacion = await _inventarioService.AddProductoSv(nuevoProducto);
    if (operacion)
    {
        return Ok("El producto ha sido ingresado exitosamente");
    }

    // Si la operación no fue exitosa, devolver mensaje más específico si es posible
    return BadRequest("Hubo un error al intentar ingresar el producto en el registro"); 
    }


    //cambiar estatus de producto
    [HttpPatch("productos/{slug}/status")]
    public async Task<IActionResult> SetEstatusProducto(string slug, [FromBody] short status)
    {
    // Convertir el slug al nombre del producto
    string nombreProducto = _inventarioService.ConvertSlugtoNombre(slug);

    if (string.IsNullOrEmpty(nombreProducto))
    {
        return NotFound("El producto no existe en los registros");
    }

    // Validar si el estado es aplicable al producto (también valida la existencia)
    var validarStatus = await _inventarioService.ValidateStatus(status, nombreProducto);
    if (!validarStatus)
    {
        return BadRequest("El estado proporcionado no es válido para el producto");
    }

    // Intentar aplicar el nuevo estado
    var estatusProducto = await _inventarioService.SetProductoStatusSv(nombreProducto, status);
    if (estatusProducto)
    {
        return Ok("El cambio de estado se ha aplicado exitosamente");
    }

    return BadRequest("El estado proporcionado ya está aplicado o el producto no existe");
    }

    //entrada y salida de productos 
    [HttpPatch("productos/{slug}/stock")]
    public async Task<IActionResult> ChangeStock(string slug, [FromBody] StockRequestDTO stockRequest)  
    {
    // Validar que la cantidad es positiva
    if (stockRequest.cantidad <= 0)
    {
        return BadRequest("La cantidad debe ser un valor positivo.");
    }

    // Convertir el slug al nombre del producto
    string nombreProducto = _inventarioService.ConvertSlugtoNombre(slug);
    if (string.IsNullOrEmpty(nombreProducto))
    {
        return NotFound("El producto no existe en los registros");
    }

    var producto = await _inventarioService.GetProductoSv(nombreProducto);
    if (producto != null)
    {
        // Validar el movimiento (entrada o salida)
        if (stockRequest.movimiento != "ENTRADA" && stockRequest.movimiento != "SALIDA")
        {
            return BadRequest("El movimiento debe ser 'ENTRADA' o 'SALIDA'.");
        }

        var movimientoStock = await _inventarioService.ChangeStockSv(nombreProducto, stockRequest.movimiento, stockRequest.cantidad);
        if (movimientoStock)
        {
            return Ok("Se ha realizado exitosamente el movimiento solicitado");
        }
        return BadRequest("Error al realizar el movimiento de stock");
    }

    return NotFound("No existe el producto que has proporcionado"); 
    
    }



    //-----endpoints para categorias--------

     //listar todas las categorias
    [HttpGet("categorias")]
    public async Task <ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias(){

        var categorias = await _inventarioService.GetAllCategoriasSv();
        return Ok(categorias);

    }

    [HttpPost("categorias")]
    public async Task<IActionResult> AddCategoria([FromBody] CategoriaDTO nuevaCategoria)
    {
    // Validación del nombre de la categoría
    if (!_inventarioService.ValidateNombreSv(nuevaCategoria.nombreCategoria))
    {
        return BadRequest("El nombre de la categoría no es válido.");
    }

    // Verificación de existencia de la categoría
    var exists = await _inventarioService.GetCategoriaSv(nuevaCategoria.nombreCategoria);
    if (exists != null)
    {
        return BadRequest("La categoría ya existe en los registros.");
    }

    // Intentar agregar la nueva categoría
    var operacion = await _inventarioService.AddCategoriaSv(nuevaCategoria);
    if (operacion)
    {
        return Ok("Categoría agregada correctamente.");
    }

    // Si la operación no fue exitosa
    return BadRequest("Hubo un error al ingresar la categoría al sistema.");
    }


    //-----endpoints para transacciones------

    [HttpGet("transacciones")]
     
    public async Task <ActionResult<IEnumerable<TransaccionDTO>>> GetTransacciones(){

        var transacciones = await _inventarioService.GetAllTransaccionesSv();
        return Ok(transacciones);

    }
    [HttpGet("transacciones/{id}")]
     
    public async Task <ActionResult<TransaccionDTO>> GetTransacciones(int id){

        var transaccion = await _inventarioService.GetTransaccionSv(id);
        if (transaccion != null){
            return Ok(transaccion);

        }
        return NotFound("no existe la trasaccion con el id proporcionado");

    }



}


    
    

    


   









