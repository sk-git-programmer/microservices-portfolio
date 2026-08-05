using Orders.Domain.Entities;
using Orders.Domain.Enums;

namespace Orders.Tests.Domain;

public class OrderTests
{
    [Fact]
    public void Constructor_WithValidCustomerName_CreatesOrder()
    {
        var order = new Order("Jan Kowalski");

        Assert.Equal("Jan Kowalski", order.CustomerName);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Empty(order.Items);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Constructor_WithInvalidCustomerName_ThrowsArgumentException(string? customerName)
    {
        Assert.Throws<ArgumentException>(() => new Order(customerName!));
    }

    [Fact]
    public void AddItem_AddsItemToOrder()
    {
        var order = new Order("Jan Kowalski");
        var item = new OrderItem("Widget", 2, 19.99m);

        order.AddItem(item);

        Assert.Single(order.Items);
        Assert.Contains(item, order.Items);
    }
}