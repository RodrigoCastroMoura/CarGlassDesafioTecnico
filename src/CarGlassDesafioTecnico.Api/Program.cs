using CarGlassDesafioTecnico.Api;
using CarGlassDesafioTecnico.Api.Infra.Configurations;
using CarGlassDesafioTecnico.Application.Usecases.Usuarios.Read;
using CarGlassDesafioTecnico.Domain.Function;
using CarGlassDesafioTecnico.Domain.Interface.Functions;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();


builder.Services.AddScoped<IDecomposicaoNumeroFunction, DecomposicaoNumeroFunction>();
builder.Services.AddScoped<IDecomporNumeroUsecases, DecomporNumeroUsecases>();


var app = builder.Build();

app.UseCustomSwagger();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
  
    endpoints.MapControllers();
});


await app.RunAsync();

public partial class Program { }
