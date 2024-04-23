using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


using CryptoApp.Business;
using CryptoApp.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ServerDB");

builder.Services.AddScoped<IUserRepository, UserSqlRepository>(serviceProvider => 
    new UserSqlRepository(connectionString));

// Add services to the container.
builder.Services.AddControllers();

// Configurar CORS para permitir todas las solicitudes
builder.Services.AddCors(
    
    /*options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
}*/
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();
builder.Services.AddScoped<ICryptoService, CryptoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    // Configurar CORS
    app.UseCors();

    // Configurar Swagger
    app.UseSwagger();
    app.UseSwaggerUI(
        
     /*   c =>
    {
        // Especificar la URL base de Swagger UI con esquema 'https'
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty; // Esto elimina el prefijo de la ruta de Swagger UI
    }
    */
    );

 /*   // Redireccionar automáticamente al iniciar la aplicación
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger");
            return;
        }

        await next();
    });*/

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
