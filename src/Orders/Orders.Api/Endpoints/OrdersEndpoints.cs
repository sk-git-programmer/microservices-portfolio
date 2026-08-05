using Orders.Api.Contracts;
using Orders.Api.Services;

namespace Orders.Api.Endpoints;

public static class OrdersEndpoints
{
    public static void MapOrdersEndpoints(this WebApplication app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, OrderService orderService, CancellationToken cancellationToken) =>
        {
            var response = await orderService.CreateOrderAsync(request, cancellationToken);
            return Results.Created($"/orders/{response.Id}", response);
        });

        app.MapGet("/orders/{id:guid}", async (Guid id, OrderService orderService, CancellationToken cancellationToken) =>
        {
            var response = await orderService.GetOrderByIdAsync(id, cancellationToken);
            return response is not null ? Results.Ok(response) : Results.NotFound();
        });

        app.MapGet("/orders", async (OrderService orderService, CancellationToken cancellationToken) =>
        {
            var orders = await orderService.GetAllOrdersAsync(cancellationToken);
            return Results.Ok(orders);
        });
    }
}