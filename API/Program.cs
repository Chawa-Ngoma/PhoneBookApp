using API.Data;
using API.Managers;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseSqlite("Data Source=Data/contacts.db"));

builder.Services.AddScoped<IContactManager, ContactManager>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Automatically create DB if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContactDbContext>();
    db.Database.EnsureCreated(); 
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
