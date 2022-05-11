using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages
{
    public partial class Cart
    {
        
        List<CartProductResponse> cartProducts = null;
        string message = "Loading cart...";
        bool isAuthenticated = false;

        protected override async Task OnInitializedAsync()
        {
            isAuthenticated = await AuthService.IsUserAuthenticated();
            await ProductService.GetProducts();
            await LoadCart();
        }
        
        private async Task RemoveProductFromCart(int productId, int productTypeId)
        {
            await CartService.RemoveProductFromCart(productId, productTypeId);
            await LoadCart();
        }

        private async Task LoadCart()
        {
            await CartService.GetCartItemsCount();
            cartProducts = await CartService.GetCartProducts();
            if (cartProducts == null || cartProducts.Count == 0)
            {
                message = "Your cart is empty.";
            }
        }

        private async Task UpdateQuantity(ChangeEventArgs e, CartProductResponse product)
        {
            product.Quantity = int.Parse(e.Value.ToString());
            if (product.Quantity < 1)
                product.Quantity = 1;
            await CartService.UpdateQuantity(product);
        }

        private async Task Checkout()
        {
            if (isAuthenticated)
            {
                NavigationManager.NavigateTo("/checkout");
            }
            else
            {
                NavigationManager.NavigateTo("/login", true);
            }

        }
        
    }
}
