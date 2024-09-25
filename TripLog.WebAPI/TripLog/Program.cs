using Microsoft.EntityFrameworkCore;
using TripLog.Context;
using TripLog.Repository;
using TripLog.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddControllers();
builder.Services.AddTransient<TripRepository>();
builder.Services.AddTransient<TagRepository>();

builder.Services.AddTransient<TripService>();
builder.Services.AddTransient<TagService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true; // isteğe bağlı olarak JSON'u daha okunabilir hale getirmek için
    });

var app = builder.Build();
app.MapControllers();
app.UseStaticFiles();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.Run();
