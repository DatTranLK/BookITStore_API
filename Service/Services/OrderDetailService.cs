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
        private readonly IComboBookRepository _comboBookRepository;
        private readonly IEBookRepository _eBookRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IComboBookRepository comboBookRepository, IEBookRepository eBookRepository, IOrderRepository orderRepository, IBookRepository bookRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _comboBookRepository = comboBookRepository;
            _eBookRepository = eBookRepository;
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
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

        public async Task<ServiceResponse<int>> CreateOrderDetailWithPhysicalBook(OrderDetail orderDetail)
        {
            try
            {
                var checkOrderExist = await _orderRepository.GetById(orderDetail.OrderId);
                if (checkOrderExist == null)
                {
                    return new ServiceResponse<int>
                    {
                        Message = "Not found order",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (orderDetail.BookId != null)
                {
                    var checkBookExist = await _bookRepository.GetById(orderDetail.BookId);
                    if (checkBookExist == null)
                    {
                        return new ServiceResponse<int>
                        {
                            Message = "Not found book",
                            Success = true,
                            StatusCode = 200
                        };
                    }
                    orderDetail.EbookId = null;
                    orderDetail.ComboBookId = null;
                }
                else if (orderDetail.EbookId != null)
                {
                    var checkEBookExist = await _eBookRepository.GetByWithCondition(x => x.EbookId == orderDetail.EbookId, null, true);
                    if (checkEBookExist == null)
                    {
                        return new ServiceResponse<int>
                        {
                            Message = "Not found ebook",
                            Success = true,
                            StatusCode = 200
                        };
                    }
                    orderDetail.BookId = null;
                    orderDetail.ComboBookId = null;
                }
                else if (orderDetail.ComboBookId != null)
                {
                    var checkComboExist = await _comboBookRepository.GetById(orderDetail.ComboBookId);
                    if (checkComboExist == null)
                    {
                        return new ServiceResponse<int>
                        {
                            Message = "Not found combo book",
                            Success = true,
                            StatusCode = 200
                        };
                        
                    }
                    orderDetail.BookId = null;
                    orderDetail.EbookId = null;
                }
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
                    x => x.ComboBook,
                    x => x.Ebook,
                    x => x.Ebook.Book
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
