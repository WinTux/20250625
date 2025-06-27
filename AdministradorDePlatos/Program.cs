using AdministradorDePlatos.Conexion;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PlatoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MiConexionLocalSqLite")));
var app = builder.Build();
app.MapGet("api/plato", async (PlatoDbContext contexto) => { 
    var elementos = await contexto.Platos.ToListAsync();
    return Results.Ok(elementos);
});
app.Run();
