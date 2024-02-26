using System.Diagnostics;
using System.Text.Json;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using UserService.Core;
using UserService.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddDapr(builder => builder.UseJsonSerializationOptions(new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true
}));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    var listener = new DiagnosticListener("Autofac");
    containerBuilder.RegisterInstance(listener).As<DiagnosticListener>().SingleInstance();
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Domain Services;
DomainDependencies.RegisterDependencies(builder.Services, builder.Configuration);
// Database Services;
DatabaseDependencies.RegisterDependencies(builder.Services, builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();
app.MapControllers();

app.Run();