using AutoMapper;
using Entity.Dtos.Order;
using Entity.Enum;
using Entity.Models;
using Repository.IRepositories;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<ServiceResponse<int>> CountOrdersForAdmin()
        {
            try
            {
                var count = await _orderRepository.CountAll(null);
                if (count <= 0)
                {
                    return new ServiceResponse<int>
                    { 
                        Data = 0,
                        Message = "Successfully",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<int>
                {
                    Data = count,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CountOrdersForCus(int cusId)
        {
            try
            {
                var count = await _orderRepository.CountAll(x => x.CustomerId == cusId);
                if (count <= 0)
                {
                    return new ServiceResponse<int>
                    {
                        Data = 0,
                        Message = "Successfully",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<int>
                {
                    Data = count,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CreateNewOrder(Order order)
        {
            try
            {
                //validation in here
                //starting insert into Db
                OrderStatus processing = OrderStatus.In_Progress;
                order.CreateDate = DateTime.Now;
                order.PaymentMethod = "Thanh toán khi nhận hàng";
                order.OrderStatus = processing.ToString();
                await _orderRepository.Insert(order);
                return new ServiceResponse<int>
                {
                    Data = order.Id,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<OrderDtoForAdmin>> GetOrderByIdForAdmin(int orderId)
        {
            try
            {
                List<Expression<Func<Order, object>>> includes = new List<Expression<Func<Order, object>>>
                {
                    x => x.Customer
                };
                var order = await _orderRepository.GetByWithCondition(x => x.Id == orderId, includes, true);
                var _mapper = config.CreateMapper();
                var orderDto = _mapper.Map<OrderDtoForAdmin>(order);
                if (order == null)
                {
                    return new ServiceResponse<OrderDtoForAdmin>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<OrderDtoForAdmin>
                {
                    Data = orderDto,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<OrderDtoForCus>> GetOrderByIdForCus(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetByWithCondition(x => x.Id == orderId, null, true);
                var _mapper = config.CreateMapper();
                var orderDto = _mapper.Map<OrderDtoForCus>(order);
                if (order == null)
                {
                    return new ServiceResponse<OrderDtoForCus>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<OrderDtoForCus>
                {
                    Data = orderDto,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<OrderDtoForAdmin>>> GetOrdersForAdminWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<Order, object>>> includes = new List<Expression<Func<Order, object>>>
                { 
                    x => x.Customer
                };
                var lst = await _orderRepository.GetAllWithPagination(null, includes, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<OrderDtoForAdmin>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<OrderDtoForAdmin>>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<OrderDtoForAdmin>>
                {
                    Data = lstDto,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<OrderDtoForCus>>> GetOrdersForCusWithPagination(int cusId, int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                var lst = await _orderRepository.GetAllWithPagination(x => x.CustomerId == cusId, null, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<OrderDtoForCus>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<OrderDtoForCus>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<OrderDtoForCus>>
                {
                    Data = lstDto,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
