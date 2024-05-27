global using APIWebCS5.Models;
global using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DogAndCatContext>(a => a.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
builder.Services.AddCors(a => a.AddPolicy("APIWebCS5", builder =>
{
    builder.WithOrigins("*").WithMethods().AllowAnyHeader().AllowAnyMethod();
}));
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
app.UseCors("APIWebCS5");
app.UseRouting();
app.UseHttpsRedirection();


app.Run();
