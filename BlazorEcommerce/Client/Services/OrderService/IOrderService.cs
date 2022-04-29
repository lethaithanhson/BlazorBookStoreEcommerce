namespace BlazorEcommerce.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task<string> PlaceOrder();
        Task<List<OrderOverviewResponse>> GetOrders();
        Task<OrderDetailsResponse> GetOrderDetails(int orderId);
        Task<bool> AddOrder(List<CartProductResponse> cartProducts);
        Task<List<OrderOverviewResponse>> GetAdminOrder();
        Task<List<OrderOverviewResponse>> GetTurnover();
    }
}
