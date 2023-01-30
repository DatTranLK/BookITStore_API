using AutoMapper;
using Entity.Dtos.OrderDetail;
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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<ServiceResponse<int>> CountAll(int orderId)
        {
            try
            {
                var count = await _orderDetailRepository.CountAll(x => x.OrderId == orderId);
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

        public async Task<ServiceResponse<int>> CreateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                //Validation in here
                //Starting insert to Db
                await _orderDetailRepository.Insert(orderDetail);
                return new ServiceResponse<int>
                {
                    Data = orderDetail.Id,
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

        public async Task<ServiceResponse<IEnumerable<OrderDetailDto>>> GetOrderDetailsById(int orderId)
        {
            try
            {
                List<Expression<Func<OrderDetail, object>>> includes = new List<Expression<Func<OrderDetail, object>>> 
                { 
                    x => x.Order,
                    x => x.Book,
                    x => x.ComboBook
                };
                var orderDetails = await _orderDetailRepository.GetAllWithCondition(x => x.OrderId == orderId, includes, null, true);
                var _mapper = config.CreateMapper();
                var orderDetailsDto = _mapper.Map<IEnumerable<OrderDetailDto>>(orderDetails);
                if (orderDetails.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<OrderDetailDto>>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<OrderDetailDto>>
                {
                    Data = orderDetailsDto,
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

        public async Task<ServiceResponse<OrderDetail>> UpdateOrderDetail(int id, OrderDetail orderDetail)
        {
            try
            {
                var checkExist = await _orderDetailRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<OrderDetail>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(orderDetail.OrderId.ToString()))
                {
                    checkExist.OrderId = orderDetail.OrderId;
                }
                if (!string.IsNullOrEmpty(orderDetail.BookId.ToString()))
                {
                    checkExist.BookId = orderDetail.BookId;
                }
                if (!string.IsNullOrEmpty(orderDetail.ComboBookId.ToString()))
                {
                    checkExist.ComboBookId = orderDetail.ComboBookId;
                }
                if (!string.IsNullOrEmpty(orderDetail.Quantity.ToString()))
                {
                    checkExist.Quantity = orderDetail.Quantity;
                }
                await _orderDetailRepository.Update(checkExist);
                return new ServiceResponse<OrderDetail>
                {
                    Data = checkExist,
                    Message = "Successfully",
                    StatusCode = 204,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
