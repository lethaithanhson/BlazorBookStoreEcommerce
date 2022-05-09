using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponse>>>> GetOrders()
        {
            var result = await _orderService.GetOrders();
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<ServiceResponse<OrderDetailsResponse>>> GetOrdersDetails(int orderId)
        {
            var result = await _orderService.GetOrderDetails(orderId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> AddOrder(List<CartProductResponse> CartItems)
        {
            var result = await _orderService.AddOrder(CartItems);
            return Ok(result);
        }
        [HttpGet("admin"), Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponse>>>> GetAdminOrder()
        {
            var result = await _orderService.GetAdminOrder();
            return Ok(result);
        }
        [HttpGet("admin/turnover"), Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponse>>>> GetTurnover()
        {
            var result = await _orderService.GetTurnover();
            return Ok(result);
        }
    }
}
