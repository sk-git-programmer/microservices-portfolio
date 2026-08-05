namespace Orders.Api.Contracts;

public record CreateOrderRequest(string CustomerName, List<CreateOrderItemRequest> Items);

public record CreateOrderItemRequest(string ProductName, int Quantity, decimal UnitPrice);