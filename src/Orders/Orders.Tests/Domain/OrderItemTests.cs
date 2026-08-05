using Orders.Domain.Entities;

namespace Orders.Tests.Domain;

public class OrderItemTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesOrderItem()
    {
        var item = new OrderItem("Widget", 2, 19.99m);

        Assert.Equal("Widget", item.ProductName);
        Assert.Equal(2, item.Quantity);
        Assert.Equal(19.99m, item.UnitPrice);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Constructor_WithInvalidProductName_ThrowsArgumentException(string? productName)
    {
        Assert.Throws<ArgumentException>(() => new OrderItem(productName!, 1, 10m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WithInvalidQuantity_ThrowsArgumentException(int quantity)
    {
        Assert.Throws<ArgumentException>(() => new OrderItem("Widget", quantity, 10m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WithInvalidUnitPrice_ThrowsArgumentException(decimal unitPrice)
    {
        Assert.Throws<ArgumentException>(() => new OrderItem("Widget", 1, unitPrice));
    }
}