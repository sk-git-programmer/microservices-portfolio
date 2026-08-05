using Orders.Api.Contracts;
using Orders.Domain.Entities;
using Orders.Infrastructure.Persistence;

namespace Orders.Api.Services;

public class OrderService
{
    private readonly OrdersDbContext _dbContext;

    public OrderService(OrdersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerName);

        foreach (var itemRequest in request.Items)
        {
            var item = new OrderItem(itemRequest.ProductName, itemRequest.Quantity, itemRequest.UnitPrice);
            order.AddItem(item);
        }

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return order;
    }
}