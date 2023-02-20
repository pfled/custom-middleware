using System.Globalization;
using MiddlewareWeb;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseAuth();

app.Run(async (context) =>
{
    await context.Response.WriteAsync(
        $"Authorized. Welcome!");
});

app.Run();