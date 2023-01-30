using Entity.Dtos.OrderDetail;
using Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Bookstore_API_JFF.Controllers
{
    [Route("api/order-details")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        [HttpGet("{orderId}", Name = "GetOrderDetailsByOrderId")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<OrderDetailDto>>>> GetOrderDetailsByOrderId(int orderId)
        {
            try
            {
                var res = await _orderDetailService.GetOrderDetailsById(orderId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpGet("count/{orderId}", Name = "CountOrderDetails")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountOrderDetails(int orderId)
        {
            try
            {
                var res = await _orderDetailService.CountAll(orderId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("order-detail", Name = "CreateOrderDetail")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<int>>> CreateOrderDetail([FromBody] OrderDetail orderDetail)
        {
            try
            {
                var res = await _orderDetailService.CreateOrderDetail(orderDetail);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("order-detail", Name = "UpdateOrderDetail")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<OrderDetail>>> UpdateOrderDetail(int id, [FromBody] OrderDetail orderDetail)
        {
            try
            {
                var res = await _orderDetailService.UpdateOrderDetail(id, orderDetail);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
