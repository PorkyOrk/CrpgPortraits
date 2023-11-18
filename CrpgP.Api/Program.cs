using CrpgP.Api;
using CrpgP.Application;
using CrpgP.Infrastructure;
using System.Data;
using Npgsql;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
//var connectionString = config.GetConnectionString("PostgresDBConnection");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddTransient<IConfiguration>(_=> config);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints();

app.UseHttpsRedirection();

app.Run();
