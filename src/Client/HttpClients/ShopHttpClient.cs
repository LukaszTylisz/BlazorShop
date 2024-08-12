using Shared.DTOs;
using Shared.Enums;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client.HttpClients;

public class ShopHttpClient(HttpClient httpClient)
{
    public async Task<List<OrderDto>> GetOrders()
    {
        var items = await httpClient.GetFromJsonAsync<List<OrderDto>>("api/orders");
        return items;
    }

    public async Task<OrderDto> GetOrderById(Guid id)
    {
        var item = await httpClient.GetFromJsonAsync<OrderDto>($"api/Orders/{id}");
        return item;
    }

    public async Task ChangeOrderStatus(Guid id, OrderStatus orderStatus)
    {
        var response = await httpClient.PutAsJsonAsync($"api/Orders/{id}/Status", Enum.GetName(typeof(OrderStatus), orderStatus));
        response.EnsureSuccessStatusCode();
    }

    public async Task<Guid> CreateOrder(CreateOrderDto order)
    {
        var response = await httpClient.PostAsJsonAsync($"api/Orders", order);
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<JsonElement>();
        var id = data.GetProperty("id").GetGuid();
        return id;
    }

    public async Task<List<ProductDto>> GetProducts()
    {
        var items = await httpClient.GetFromJsonAsync<List<ProductDto>>("api/Products");
        return items;
    }

    public async Task<ProductDto> GetProductById(Guid id)
    {
        var item = await httpClient.GetFromJsonAsync<ProductDto>($"api/Products/{id}");
        return item;
    }

    public async Task<ProductDto> FindProductById(Guid id)
    {
        var response = await httpClient.GetAsync($"api/Products/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        return null;
    }

    public async Task<Guid> CreateProduct(CreateProductDto product)
    {
        var response = await httpClient.PostAsJsonAsync($"api/Products", product);
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<JsonElement>();
        var id = data.GetProperty("id").GetGuid();
        return id;
    }

    public async Task UpdateProduct(UpdateProductDto product)
    {
        var response = await httpClient.PutAsJsonAsync($"api/Products/{product.Id}", product);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteProduct(Guid id)
    {
        var response = await httpClient.DeleteAsync($"api/Products/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<CustomerDto>> GetCustomers()
    {
        var items = await httpClient.GetFromJsonAsync<List<CustomerDto>>("api/Customers");
        return items;
    }

    public async Task<CustomerDto> GetCustomerById(Guid id)
    {
        var item = await httpClient.GetFromJsonAsync<CustomerDto>($"api/Customers/{id}");
        return item;
    }

    public async Task<Guid> CreateCustomer(CreateCustomerDto customer)
    {
        var response = await httpClient.PostAsJsonAsync($"api/Customers", customer);
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<JsonElement>();
        var id = data.GetProperty("id").GetGuid();
        return id;
    }

    public async Task UpdateCustomer(UpdateCustomerDto customer)
    {
        var response = await httpClient.PutAsJsonAsync($"api/Customers/{customer.Id}", customer);
        response.EnsureSuccessStatusCode();
    }
}
