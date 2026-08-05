using Orders.Api.Contracts;
using Orders.Api.Services;

namespace Orders.Api.Endpoints;

public static class OrdersEndpoints
{
    public static void MapOrdersEndpoints(this WebApplication app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, OrderService orderService, CancellationToken cancellationToken) =>
        {
            var order = await orderService.CreateOrderAsync(request, cancellationToken);
            return Results.Created($"/orders/{order.Id}", order);
        });
    }
}