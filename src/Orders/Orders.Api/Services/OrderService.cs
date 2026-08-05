using Microsoft.EntityFrameworkCore;
using Orders.Api.Contracts;
using Orders.Domain.Entities;
using Orders.Infrastructure.Persistence;

namespace Orders.Api.Services;

public class OrderService
{
    private readonly OrdersDbContext _dbContext;
    private readonly ILogger<OrderService> _logger;

    public OrderService(OrdersDbContext dbContext, ILogger<OrderService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
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

        _logger.LogInformation("Order {OrderId} created for customer {CustomerName} with {ItemCount} items",
            order.Id, order.CustomerName, order.Items.Count);

        return order.ToResponse();
    }

    public async Task<OrderResponse?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        return order?.ToResponse();
    }

    public async Task<List<OrderResponse>> GetAllOrdersAsync(CancellationToken cancellationToken)
    {
        var orders = await _dbContext.Orders
            .Include(o => o.Items)
            .ToListAsync(cancellationToken);

        return orders.Select(o => o.ToResponse()).ToList();
    }
}