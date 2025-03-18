using Application;
using Domain.Abstractions.IUnitOfWork;
using Microsoft.EntityFrameworkCore;
using Persistance.InMemoryDB;
using Persistance.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
Assembly applicationAssembly = typeof(Application.ApplicationAssembly).Assembly;
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly));
builder.Services.AddDbContext<InMemoryDB>(options =>
                   options.UseInMemoryDatabase("InMemoryDb"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue",
        policy =>
        {
            policy.WithOrigins("http://localhost:8080", "http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowVue");
app.MapControllers();

app.Run();
