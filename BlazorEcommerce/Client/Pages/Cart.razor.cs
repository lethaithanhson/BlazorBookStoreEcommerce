using Microsoft.AspNetCore.Components;
using WMBlazorSlickCarousel.WMBSC;

namespace BlazorEcommerce.Client.Pages
{
    public partial class Cart
    {
        public BlazorSlickCarousel theCarousel;
        public WMBSCInitialSettings configurations;
        List<CartProductResponse> cartProducts = null;
        string message = "Loading cart...";
        bool isAuthenticated = false;

        protected override async Task OnInitializedAsync()
        {
            isAuthenticated = await AuthService.IsUserAuthenticated();
            await ProductService.GetProducts();
            await LoadCart();
        }
        
        protected override void OnInitialized()
        {
            ProductSliderSetting();
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
        
        private void ProductSliderSetting()
        {
            // to <= 992px screen
            WMBSCSettings breakpoint992Settings = new WMBSCSettings
            {
                autoplay = true,
                arrows = false,
                slidesToShow = 2,
            };
            WMBSCResponsiveSettings breakpoint992Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 992,
                settings = breakpoint992Settings
            };

            // to <= 768px screen
            WMBSCSettings breakpoint768Settings = new WMBSCSettings
            {
                autoplay = true,
                arrows = false,
                slidesToShow = 3
            };
            WMBSCResponsiveSettings breakpoint768Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 768,
                settings = breakpoint768Settings
            };

            // to <= 575px screen
            WMBSCSettings breakpoint575Settings = new WMBSCSettings
            {
                autoplay = true,
                arrows = false,
                slidesToShow = 2
            };
            WMBSCResponsiveSettings breakpoint575Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 575,
                settings = breakpoint575Settings
            };

            // to <= 480px screen
            WMBSCSettings breakpoint480Settings = new WMBSCSettings
            {
                autoplay = true,
                arrows = false,
                slidesToShow = 1
            };
            WMBSCResponsiveSettings breakpoint480Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 480,
                settings = breakpoint480Settings
            };

            // to <= 320px screen
            WMBSCSettings breakpoint320Settings = new WMBSCSettings
            {
                autoplay = true,
                arrows = false,
                slidesToShow = 1
            };
            WMBSCResponsiveSettings breakpoint320Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 320,
                settings = breakpoint320Settings
            };

            // add responsivity
            List<WMBSCResponsiveSettings> responsiveSettingsList = new List<WMBSCResponsiveSettings>();
            responsiveSettingsList.Add(breakpoint992Responsive);
            responsiveSettingsList.Add(breakpoint768Responsive);
            responsiveSettingsList.Add(breakpoint575Responsive);
            responsiveSettingsList.Add(breakpoint480Responsive);
            responsiveSettingsList.Add(breakpoint320Responsive);

            configurations = new WMBSCInitialSettings
            {
                arrows = false,
                autoplay = true,
                autoplaySpeed = 8000,
                slidesToShow = 2,
                infinite = true,
                responsive = responsiveSettingsList
            };
        }
    }
}
