using DustSuckerWebApp.DataLayer;
using DustSuckerWebApp.Extensions;
using DustSuckerWebApp.ServiceLayer.AdvertisementsServices;
using DustSuckerWebApp.ServiceLayer.HoverServices;
using DustSuckerWebApp.ServiceLayer.UserServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<HooverService>();
builder.Services.AddScoped<AdvertisementService>();
builder.Services.AddScoped<UserService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Dust Sucker API",
        Version = "v1",
        Description = "Описание API",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthorization();

app.MapGet("/", () => "API is work");
app.MapControllers();

app.Run();
