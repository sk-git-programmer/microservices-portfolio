namespace Orders.Api.Contracts;

public record OrderResponse(
    Guid Id,
    string CustomerName,
    string Status,
    DateTime CreatedAt,
    List<OrderItemResponse> Items);

public record OrderItemResponse(
    Guid Id,
    string ProductName,
    int Quantity,
    decimal UnitPrice);