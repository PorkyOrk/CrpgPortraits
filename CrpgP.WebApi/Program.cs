using CrpgP.Domain.Abstractions;
using CrpgP.WebApi;
using CrpgP.Infrastructure.DataProvider.Postgres.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSingleton<IGameRepository, GameRepository>();
builder.Services.AddSingleton<ITagRepository, TagRepository>();
builder.Services.AddSingleton<IPortraitRepository, PortraitRepository>();
builder.Services.AddSingleton<ISizeRepository, SizeRepository>();



// builder.Services.Configure<ConnectionStrings>(
//     builder.Configuration.GetSection(nameof(ConnectionStrings)));



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
