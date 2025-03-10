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
        return Results.BadRequest("����� ��������� ������ �����������!");

    if (file.Length > 32 * 1024 * 1024) // 32 MB
        return Results.BadRequest("���� ������ ���� ������ ��� 32��");

    var url = await service.Add(file);

    if (url == null)
        return Results.BadRequest("�� ������� ��������� ����, ������ �� �������");

    return Results.Ok(new { ImageUrl = url, Message = "���� ������� ��������" });
}).DisableAntiforgery();

app.Run();
