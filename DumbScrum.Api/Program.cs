using DumbScrum.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Usefull Command
// Scaffold-DbContext 'Data Source=localhost;Initial Catalog=dumb_scrum_db;TrustServerCertificate=true;Integrated Security=True;Encrypt=False' Microsoft.EntityFrameworkCore.SqlServer -Context 'DumbScrumDbContext' -OutputDir Models

// Add services to the container.

// Adds Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
