using WMBlazorSlickCarousel.WMBSC;

namespace BlazorEcommerce.Client.Shared
{
    public partial class FeaturedProducts
    {
        public BlazorSlickCarousel TopBanner;
        public WMBSCInitialSettings TopBannerconfigurations;
        public WMBSCInitialSettings TopProductsconfigurations;
        public WMBSCInitialSettings ClientTestimonialsconfig;
        protected override void OnInitialized()
        {
            ProductCarouselSetting();
            BannerCarouselSetting();
            ClientTestimonialsSetting();
            ProductService.ProductsChanged += StateHasChanged; 
        }

        public void Dispose()
        {
            ProductService.ProductsChanged -= StateHasChanged;
        }


        private ProductVariant GetSelectedVariant(Product product)
        {
            var currentTypeId = product.Variants[0].ProductTypeId;
            var variant = product.Variants.FirstOrDefault(v => v.ProductTypeId == currentTypeId);
            return variant;
        }

        private async Task AddToCart(Product product)
        {
            var productVariant = GetSelectedVariant(product);
            var cartItem = new CartItem
            {
                ProductId = productVariant.ProductId,
                ProductTypeId = productVariant.ProductTypeId
            };

            await CartService.AddToCart(cartItem);
        }
        private void ClientTestimonialsSetting()
        {
            ClientTestimonialsconfig = new WMBSCInitialSettings
            {
                autoplay = true,
                dots = true,
                autoplaySpeed = 8000,
                slidesToShow = 3,
                arrows = false,
                infinite = false,
            };
        }

        private void BannerCarouselSetting()
        {
            WMBSCSettings breakpoint1200Settings = new WMBSCSettings
            {
                infinite = false,
                dots = true,
                arrows = false,
                slidesToShow = 2
            };
            WMBSCResponsiveSettings breakpoint1200Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 1200,
                settings = breakpoint1200Settings
            };

            // to <= 992px screen
            WMBSCSettings breakpoint992Settings = new WMBSCSettings
            {
                infinite = false,
                dots = true,
                arrows = false,
                slidesToShow = 1
            };
            WMBSCResponsiveSettings breakpoint992Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 992,
                settings = breakpoint992Settings
            };

            // to <= 768px screen
            WMBSCSettings breakpoint768Settings = new WMBSCSettings
            {
                infinite = false,
                dots = true,
                arrows = false,
                slidesToShow = 1
            };
            WMBSCResponsiveSettings breakpoint768Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 768,
                settings = breakpoint768Settings
            };

            // to <= 480px screen
            WMBSCSettings breakpoint480Settings = new WMBSCSettings
            {
                infinite = false,
                dots = true,
                arrows = false,
                slidesToShow = 1
            };
            WMBSCResponsiveSettings breakpoint480Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 480,
                settings = breakpoint480Settings
            };

            // add responsivity
            List<WMBSCResponsiveSettings> responsiveSettingsList = new List<WMBSCResponsiveSettings>();
            responsiveSettingsList.Add(breakpoint1200Responsive);
            responsiveSettingsList.Add(breakpoint992Responsive);
            responsiveSettingsList.Add(breakpoint768Responsive);
            responsiveSettingsList.Add(breakpoint480Responsive);
           
            TopBannerconfigurations = new WMBSCInitialSettings
            {
                autoplay = true,
                dots = true,
                autoplaySpeed = 8000,
                slidesToShow = 1,
                arrows = false,
                infinite = false,
            };
        }

        private void ProductCarouselSetting()
        {
            // to <= 1200px screen
            WMBSCSettings breakpoint1200Settings = new WMBSCSettings
            {
                infinite = false,
                dots = true,
                arrows = false,
                slidesToShow = 4
            };
            WMBSCResponsiveSettings breakpoint1200Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 1200,
                settings = breakpoint1200Settings
            };

            // to <= 992px screen
            WMBSCSettings breakpoint992Settings = new WMBSCSettings
            {
                infinite = false,
                dots = true,
                arrows = false,
                slidesToShow = 3
            };
            WMBSCResponsiveSettings breakpoint992Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 992,
                settings = breakpoint992Settings
            };

            // to <= 768px screen
            WMBSCSettings breakpoint768Settings = new WMBSCSettings
            {
                infinite = false,
                dots = true,
                arrows = false,
                slidesToShow = 2
            };
            WMBSCResponsiveSettings breakpoint768Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 768,
                settings = breakpoint768Settings
            };

            // to <= 480px screen
            WMBSCSettings breakpoint480Settings = new WMBSCSettings
            {
                infinite = false,
                dots = true,
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
                infinite = false,
                dots = true,
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
            responsiveSettingsList.Add(breakpoint1200Responsive);
            responsiveSettingsList.Add(breakpoint992Responsive);
            responsiveSettingsList.Add(breakpoint768Responsive);
            responsiveSettingsList.Add(breakpoint480Responsive);
            responsiveSettingsList.Add(breakpoint320Responsive);

            TopProductsconfigurations = new WMBSCInitialSettings
            {
                autoplay = true,
                dots = true,
                autoplaySpeed = 8000,
                slidesToShow = 5,
                arrows = false,
                infinite = false,
                responsive = responsiveSettingsList
            };
        }
    }
}
