using CrpgP.WebApi.Endpoints;
using CrpgP.WebApi.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices();

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
