using Microsoft.EntityFrameworkCore;
using Orders.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrdersDb")));

var app = builder.Build();

app.MapGet("/health", () => Results.Ok("OK"));

app.Run();