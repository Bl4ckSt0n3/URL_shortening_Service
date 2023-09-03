using Microsoft.EntityFrameworkCore;
using URL_shortening_Service.Models.Database;
using URL_shortening_Service.Services;
using URL_shortening_Service.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IShortener, ShortenerService>();
builder.Services.AddDbContext<UrlDataDbContext>(o => { o.UseSqlite($"Data Source=./UrlShortenDB.db"); });

// builder.Services.AddDbContext<UrlDataDbContext>(options => {
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
// }, ServiceLifetime.Transient);

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
