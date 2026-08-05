using Microsoft.EntityFrameworkCore;
using Orders.Api.Endpoints;
using Orders.Api.Services;
using Orders.Infrastructure.Persistence;
using FluentValidation;
using Orders.Api.Contracts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console();
});

builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrdersDb")));

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<IValidator<CreateOrderRequest>, CreateOrderRequestValidator>();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok("OK"));
app.MapOrdersEndpoints();
app.UseSerilogRequestLogging();

app.Run();

public partial class Program { }