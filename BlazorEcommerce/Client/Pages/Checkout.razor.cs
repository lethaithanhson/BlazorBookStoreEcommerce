namespace BlazorEcommerce.Client.Pages
{
    public partial class Checkout
    {
        List<CartProductResponse> cartProducts = null;
        string message = "Loading cart...";
        bool isAuthenticated = false;
        protected override async Task OnInitializedAsync()
        {
            isAuthenticated = await AuthService.IsUserAuthenticated();
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
        private async Task PlaceOrder()
        {
            if (isAuthenticated)
            {
                var order = await OrderService.AddOrder(cartProducts);
                if (order)
                {
                    await CartService.GetCartItemsCount();
                    ToastService.ShowSuccess("Check out is success!");
                    NavigationManager.NavigateTo("/order-success");
                }
                else
                {
                    ToastService.ShowError("Check out failse. Please reload this page.");
                }
            }
        }
    }
}
