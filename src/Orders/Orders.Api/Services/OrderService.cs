using Microsoft.EntityFrameworkCore;
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

    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerName);

        foreach (var itemRequest in request.Items)
        {
            var item = new OrderItem(itemRequest.ProductName, itemRequest.Quantity, itemRequest.UnitPrice);
            order.AddItem(item);
        }

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return order.ToResponse();
    }

    public async Task<OrderResponse?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        return order?.ToResponse();
    }
}