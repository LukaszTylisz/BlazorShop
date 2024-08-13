using Blazored.LocalStorage;
using Client.HttpClients;
using Client.Models;

namespace Client.Services;

public class CartService(
    ILocalStorageService localStorage,
    ShopHttpClient shopHttpClient)
{
    private const string CartStorageKey = "cart";

    public async Task<Cart> GetShoppingCart()
    {
        var cart = await localStorage.GetItemAsync<Cart>(CartStorageKey);
        if (cart == null)
            return new Cart();
        return cart;
    }

    public async Task AddProductToShoppingCart(Guid productId)
    {
        var cart = await GetShoppingCart();

        var cartItem = cart.CartItems.SingleOrDefault(x => x.ProductId == productId);

        if (cartItem == null)
        {
            var product = await shopHttpClient.GetProductById(productId);
            cart.CartItems.Add(new CartItem
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            });
        }
        else
        {
            cartItem.Quantity++;
        }

        await localStorage.SetItemAsync(CartStorageKey, cart);
    }

    public async Task RemoveProductFromShoppingCart(Guid productId)
    {
        var cart = await GetShoppingCart();

        var cartItem = cart.CartItems.SingleOrDefault(x => x.ProductId == productId);

        if (cartItem != null)
        {
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
            }
            else
            {
                cart.CartItems.Remove(cartItem);
            }
        }

        await localStorage.SetItemAsync(CartStorageKey, cart);
    }

    public async Task EmptyCart()
    {
        await localStorage.RemoveItemAsync(CartStorageKey);
    }

    public async Task RefreshShoppingCart()
    {
        var cart = await GetShoppingCart();

        foreach (var cartItem in cart.CartItems.ToList())
        {
            var product = await shopHttpClient.FindProductById(cartItem.ProductId);
            if (product != null)
            {
                cartItem.Price = product.Price;
                cartItem.ProductName = product.Name;
            }
            else
            {
                cart.CartItems.Remove(cartItem);
            }
        }

        await localStorage.SetItemAsync(CartStorageKey, cart);
    }
}
