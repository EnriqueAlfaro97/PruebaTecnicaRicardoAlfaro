using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using PruebaTecnicaRicardoAlfaro.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuré la conexión a SQL Server utilizando la cadena definida en appsettings.json

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Implementé esta opción para evitar errores de ciclos infinitos al serializar relaciones bidireccionales entre Usuarios y Tareas
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Configuré Swagger para generar la documentación automática de mis endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prueba Técnica Ricardo Alfaro", Version = "v1", Description = "API para la gestión de usuarios y tareas" });
});

var app = builder.Build();

// Configuré la ruta personalizada para cumplir con el requerimiento técnico
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "api-docs/{documentName}/swagger.json";
    });

    app.UseSwaggerUI(c =>
    {
        // Esto hace que Swagger cargue en http://localhost:5000/api-docs
        c.SwaggerEndpoint("/api-docs/v1/swagger.json", "Prueba Técnica Ricardo Alfaro - API v1");
        c.RoutePrefix = "api-docs";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Implementé este bloque para asegurar que las migraciones de la base de datos se apliquen automáticamente al levantar el contenedor de Docker
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();
