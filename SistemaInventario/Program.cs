using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add appsettings.Local.json to the configuration
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddDbContext<InventarioContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("InventarioConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<InterfaceRepository, InventarioRepository>();
builder.Services.AddScoped<InterfaceServices, InventarioService>();
builder.Services.AddScoped<InterfaceMapper, InventarioMapper>();

// Agregar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Inventario",
        Version = "v1",
        Description = "Una API para gestionar productos e inventarios."
    });
});

builder.Services.AddControllers();

// Configure the HTTP request pipeline.
var app = builder.Build();

// Configurar la ejecución de Swagger y la UI de Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Inventario v1");
        c.RoutePrefix = string.Empty;  // Esto hace que Swagger UI esté en la raíz, por ejemplo, https://localhost:5001/
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();