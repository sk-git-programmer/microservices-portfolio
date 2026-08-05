using Microsoft.EntityFrameworkCore;
using Orders.Api.Endpoints;
using Orders.Api.Services;
using Orders.Infrastructure.Persistence;
using FluentValidation;
using Orders.Api.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrdersDb")));

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<IValidator<CreateOrderRequest>, CreateOrderRequestValidator>();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok("OK"));
app.MapOrdersEndpoints();

app.Run();

public partial class Program { }