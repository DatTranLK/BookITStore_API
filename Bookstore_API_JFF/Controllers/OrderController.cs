﻿using Entity.Dtos.Order;
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
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("admin", Name = "GetOrdersForAdmin")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<OrderDtoForAdmin>>>> GetOrdersForAdmin([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _orderService.GetOrdersForAdminWithPagination(page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpGet("admin/count", Name = "CountOrdersForAdmin")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountOrdersForAdmin()
        {
            try
            {
                var res = await _orderService.CountOrdersForAdmin();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("admin/order/{orderId}", Name = "GetOrderByIdForAdmin")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<OrderDtoForAdmin>>> GetOrderByIdForAdmin(int orderId)
        {
            try
            {
                var res = await _orderService.GetOrderByIdForAdmin(orderId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("customer/{cusId}", Name = "GetOrdersForCus")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<OrderDtoForCus>>>> GetOrdersForCus(int cusId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _orderService.GetOrdersForCusWithPagination(cusId, page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("customer/{cusId}/count", Name = "CountOrdersForCus")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountOrdersForCus(int cusId)
        {
            try
            {
                var res = await _orderService.CountOrdersForCus(cusId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("customer/order/{orderId}", Name = "GetOrderByIdForCus")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<OrderDtoForCus>>> GetOrderByIdForCus(int orderId)
        {
            try
            {
                var res = await _orderService.GetOrderByIdForCus(orderId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("order", Name = "CreateNewOrder")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<int>>> CreateNewOrder([FromBody]Order order)
        {
            try
            {
                var res = await _orderService.CreateNewOrder(order);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("order/order-status/accepted", Name = "ChangeOrderStatusToAcceptedWithOCDMethod")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> ChangeOrderStatusToAcceptedWithOCDMethod(int orderId)
        {
            try
            {
                var res = await _orderService.ChangeOrderStatusToAcceptedWithOCDMethod(orderId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("order/order-status/cancel", Name = "ChangeOrderStatusToCancel")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> ChangeOrderStatusToCancel(int orderId)
        {
            try
            {
                var res = await _orderService.ChangeOrderStatusToCancel(orderId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("order/order-status/paid", Name = "ChangeOrderStatusToPaid")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> ChangeOrderStatusToPaid(int orderId)
        {
            try
            {
                var res = await _orderService.ChangeOrderStatusToPaidWithOCDMethod(orderId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("order/order-status/done", Name = "ChangeOrderStatusToDone")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> ChangeOrderStatusToDone(int orderId)
        {
            try
            {
                var res = await _orderService.ChangeOrderStatusToDone(orderId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
