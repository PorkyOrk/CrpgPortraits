using CrpgP.WebApi.Endpoints;
using CrpgP.WebApi.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration);

// Remove default logging providers
builder.Logging.ClearProviders();

// Serilog
builder.Host.UseSerilog((hostContext, services, configuration) => 
    configuration.ReadFrom.Configuration(hostContext.Configuration));

// Build the application
var app = builder.Build();

// Request Logging Middleware for development environment
if (app.Environment.IsDevelopment())
{
    app.UseSerilogRequestLogging();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints();

app.UseHttpsRedirection();

app.Run();
