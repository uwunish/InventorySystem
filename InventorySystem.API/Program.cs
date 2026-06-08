using InventorySystem.Application;
using InventorySystem.Infrastructure;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastucture(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    policy.WithOrigins(
        "http://localhost:4200",
        )
    .AllowAnyHeader()
    .AllowAnyMethod()
    );
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Angular"); // should be before auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
