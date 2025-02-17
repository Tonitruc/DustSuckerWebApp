using DustSuckerWebApp.DataLayer;
using DustSuckerWebApp.Extensions;
using DustSuckerWebApp.ServiceLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<HooverService>();
builder.Services.AddScoped<AdvertisementService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapGet("/", () => "API is work");
app.MapControllers();

app.Run();
