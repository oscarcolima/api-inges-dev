using Microsoft.EntityFrameworkCore;
using api_inges_dev.Context;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ConexionSQLServer>(opt =>
    opt.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("sqlConnection")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options
    .WithOrigins(new[] { "*" }) // Reemplaza "*" con los orígenes permitidos
    .WithMethods(new[] { "GET", "POST", "PUT", "DELETE" }) // Especifica los métodos HTTP permitidos
    .AllowAnyHeader()); // Permite cualquier encabezado


app.Run();
