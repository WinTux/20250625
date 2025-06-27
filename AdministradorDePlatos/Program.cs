using AdministradorDePlatos.Conexion;
using AdministradorDePlatos.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PlatoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MiConexionLocalSqLite")));
var app = builder.Build();
app.MapGet("api/plato", async (PlatoDbContext contexto) => { 
    var elementos = await contexto.Platos.ToListAsync();
    return Results.Ok(elementos);
});

app.MapPost("api/plato", async (PlatoDbContext contexto, Plato plato) => {
    await contexto.Platos.AddAsync(plato);
    await contexto.SaveChangesAsync();
    return Results.Created($"api/plato/{plato.Id}",plato);
});

app.MapPut("api/plato/{id}", async (PlatoDbContext contexto, int id, Plato plato) => {
    var platoEnDDBB = await contexto.Platos.FirstOrDefaultAsync(p => p.Id == id);
    if(platoEnDDBB == null)
        return Results.NotFound();
    platoEnDDBB.Nombre = plato.Nombre;
    await contexto.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("api/plato/{id}", async (PlatoDbContext contexto, int id) => {
    var platoEnDDBB = await contexto.Platos.FirstOrDefaultAsync(p => p.Id == id);
    if (platoEnDDBB == null)
        return Results.NotFound();
    contexto.Platos.Remove(platoEnDDBB);
    await contexto.SaveChangesAsync();
    return Results.NoContent();
});
app.Run();
