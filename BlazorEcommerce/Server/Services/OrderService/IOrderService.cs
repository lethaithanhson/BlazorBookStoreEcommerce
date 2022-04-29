namespace BlazorEcommerce.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder(int userId);
        Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders();
        Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId);
        Task<ServiceResponse<bool>> AddOrder(List<CartProductResponse> cartItems);
        Task<ServiceResponse<List<OrderOverviewResponse>>> GetAdminOrder();
        Task<ServiceResponse<List<OrderOverviewResponse>>> GetTurnover();
    }
}
