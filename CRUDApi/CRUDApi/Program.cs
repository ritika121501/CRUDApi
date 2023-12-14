using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CRUDApi.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CRUDApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CRUDApiContext") ?? throw new InvalidOperationException("Connection string 'CRUDApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Piplines in asp .net core is a software pipeline which gets executed with every request
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
