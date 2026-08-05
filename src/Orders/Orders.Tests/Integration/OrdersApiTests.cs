using System.Net;
using System.Net.Http.Json;
using Orders.Api.Contracts;

namespace Orders.Tests.Integration;

public class OrdersApiTests : IClassFixture<OrdersApiFactory>
{
    private readonly HttpClient _client;

    public OrdersApiTests(OrdersApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostOrders_WithValidData_Returns201AndCreatesOrder()
    {
        var request = new CreateOrderRequest("Jan Kowalski",
            [new CreateOrderItemRequest("Widget", 2, 19.99m)]);

        var response = await _client.PostAsJsonAsync("/orders", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content.ReadFromJsonAsync<OrderResponse>();
        Assert.NotNull(created);
        Assert.Equal("Jan Kowalski", created!.CustomerName);
        Assert.Single(created.Items);
    }

    [Fact]
    public async Task PostOrders_WithInvalidData_Returns400()
    {
        var request = new CreateOrderRequest("", []);

        var response = await _client.PostAsJsonAsync("/orders", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetOrderById_WhenOrderExists_Returns200()
    {
        var request = new CreateOrderRequest("Anna Nowak",
            [new CreateOrderItemRequest("Gadget", 1, 9.99m)]);
        var createResponse = await _client.PostAsJsonAsync("/orders", request);
        var created = await createResponse.Content.ReadFromJsonAsync<OrderResponse>();

        var response = await _client.GetAsync($"/orders/{created!.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOrderById_WhenOrderDoesNotExist_Returns404()
    {
        var response = await _client.GetAsync($"/orders/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}