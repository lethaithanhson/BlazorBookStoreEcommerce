namespace BlazorEcommerce.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        List<CartProductResponse> CartProducts { get; set; }
        Task AddToCart(CartItem cartItem);
        Task<List<CartProductResponse>> GetCartProducts();
        Task RemoveProductFromCart(int productId, int productTypeId);
        Task UpdateQuantity(CartProductResponse product);
        Task StoreCartItems(bool emptyLocalCart);
        Task GetCartItemsCount();
    }
}
