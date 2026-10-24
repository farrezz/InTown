using InTown.Data;
using Microsoft.EntityFrameworkCore;
using InTown.Core.Interfaces;
using InTown.Core.Services;
using InTown.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ===== Controller and Swagger =====
// Add services to the container.
builder.Services.AddControllers();
// Configure the HTTP request pipeline.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== Database =====
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// ===== Repositories ====
builder.Services.AddScoped<IUserRepository, UserRepository>();


// ===== Services =====
builder.Services.AddScoped<IAuthService, AuthService>(); 

// ===== Auth =====


// ===== CORS =====






var app = builder.Build();


// ===== Controllers and Swagger =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();