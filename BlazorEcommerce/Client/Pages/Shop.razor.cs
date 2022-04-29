using Microsoft.JSInterop;

namespace BlazorEcommerce.Client.Pages
{
    public partial class Shop
    {
        private PagingParameters PagingParameters = new PagingParameters();
        public MetaData MetaData { get; set; } = new MetaData();
        private List<Product> listProducts;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("viewMode");
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await GetProduct();   
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

        private async Task GetProduct()
        {
            var pagingRespones = await ProductService.GetproductPaging(PagingParameters);
            listProducts = pagingRespones.Items;
            MetaData = pagingRespones.MetaData;
        }
        private async Task SelectedPage(int page)
        {
            PagingParameters.PageNumber = page;
            await GetProduct();
        }
    }
}
