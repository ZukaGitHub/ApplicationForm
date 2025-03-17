using Application;
using Domain.Abstractions.IUnitOfWork;
using Microsoft.EntityFrameworkCore;
using Persistance.InMemoryDB;
using Persistance.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
Assembly applicationAssembly = typeof(Application.ApplicationAssembly).Assembly;
// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly));
builder.Services.AddDbContext<InMemoryDB>(options =>
                   options.UseInMemoryDatabase("InMemoryDb"));
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
