using Microsoft.JSInterop;

namespace BlazorEcommerce.Client.Shared
{
    public partial class ShopNavMenu
    {
        protected override async Task OnInitializedAsync()
        {
            await CategoryService.GetCategories();
            CategoryService.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            CategoryService.OnChange -= StateHasChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    await JSRuntime.InvokeVoidAsync("dotNetToJsSamples");

                }catch (Exception ex)
                {

                }
                
            }
        }
        private void GoToHome()
        {
            NavigationManager.NavigateTo("", true);
        }
       
    }
}
