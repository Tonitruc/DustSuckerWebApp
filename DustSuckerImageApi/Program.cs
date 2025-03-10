using DustSuckerImageApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ImageBbService>();

var app = builder.Build();

app.UseRouting();

//app.MapGet("/", () => "Api is work");

app.MapGet("/", () => "Api is work");

app.MapPost("/", async (IFormFile file, [FromServices] ImageBbService service) =>
{
    if (!file.ContentType.StartsWith("image/"))
        return Results.BadRequest("Можно загружать только изображения!");

    if (file.Length > 32 * 1024 * 1024) // 32 MB
        return Results.BadRequest("Файл должен быть меньше чем 32мб");

    var url = await service.Add(file);

    if (url == null)
        return Results.BadRequest("Не удалось загрузить файл, ошибка на сервере");

    return Results.Ok(new { ImageUrl = url, Message = "Файл успешно добавлен" });
}).DisableAntiforgery();

app.Run();
