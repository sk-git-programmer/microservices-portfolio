using Orders.Domain.Entities;

namespace Orders.Api.Contracts;

public static class OrderMappingExtensions
{
    public static OrderResponse ToResponse(this Order order)
    {
        return new OrderResponse(
            order.Id,
            order.CustomerName,
            order.Status.ToString(),
            order.CreatedAt,
            order.Items.Select(i => new OrderItemResponse(i.Id, i.ProductName, i.Quantity, i.UnitPrice)).ToList());
    }
}