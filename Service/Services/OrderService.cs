﻿using AutoMapper;
using Entity;
using Entity.Dtos.Order;
using Entity.Enum;
using Entity.Models;
using Microsoft.Extensions.Configuration;
using Repository.IRepositories;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IEBookRepository _eBookRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public OrderService(IOrderRepository orderRepository, IConfiguration configuration, IAccountRepository accountRepository, IOrderDetailRepository orderDetailRepository, IBookRepository bookRepository, IEBookRepository eBookRepository)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
            _accountRepository = accountRepository;
            _orderDetailRepository = orderDetailRepository;
            _bookRepository = bookRepository;
            _eBookRepository = eBookRepository;
        }

        public async Task<ServiceResponse<string>> ChangeOrderStatusToAcceptedWithOCDMethod(int orderId)
        {
            try
            {
                var checkOrderExist = await _orderRepository.GetById(orderId);
                if (checkOrderExist == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Not found order",
                        Success = true,
                        StatusCode = 200
                    };
                }
                
                OrderStatus accepted = OrderStatus.Accepted;
                OrderStatus processing = OrderStatus.In_Progress;
                if (!checkOrderExist.OrderStatus.Equals(processing.ToString()))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Can not change the status",
                        Success = true,
                        StatusCode = 400
                    };
                }
                else 
                {
                    var lstOrderDetailFromOrderId = await _orderDetailRepository.GetByCondition(x => x.OrderId == orderId);
                    if (lstOrderDetailFromOrderId.Count() <= 0)
                    {
                        return new ServiceResponse<string>
                        {
                            Message = "Order details have no rows",
                            Success = true,
                            StatusCode = 400
                        };
                    }
                    else
                    {
                        foreach (var item in lstOrderDetailFromOrderId)
                        {
                            if (item.BookId != null)
                            {
                                var book = await _bookRepository.GetById(item.BookId);
                                book.Amount -= item.Quantity;
                                book.AmountSold += item.Quantity;
                                await _bookRepository.Save();
                            }
                            else if (item.EbookId != null)
                            {
                                var ebook = await _eBookRepository.GetByWithCondition(x => x.EbookId == item.EbookId, null, true);
                                var book = await _bookRepository.GetById(ebook.BookId);
                                book.AmountSold += item.Quantity;
                                await _bookRepository.Save();
                            }
                            else if (item.ComboBookId != null)
                            {
                                Console.WriteLine("Hello co combobook");
                            }
                        }
                    }
                    checkOrderExist.OrderStatus = accepted.ToString();
                    await _orderRepository.Save();
                    return new ServiceResponse<string>
                    { 
                        Message = "Successfully",
                        Success = true,
                        StatusCode = 204
                    };
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<ServiceResponse<string>> ChangeOrderStatusToCancel(int orderId)
        {
            try
            {
                var checkOrderExist = await _orderRepository.GetById(orderId);
                if (checkOrderExist == null)
                {
                    return new ServiceResponse<string>
                    { 
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                OrderStatus processing = OrderStatus.In_Progress;
                OrderStatus cancel = OrderStatus.Cancle;
                if (checkOrderExist.OrderStatus.Equals(processing.ToString()))
                {
                    checkOrderExist.OrderStatus = cancel.ToString();
                    await _orderRepository.Save();
                }
                else 
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Can not change the status",
                        Success = true,
                        StatusCode = 400
                    };
                }
                return new ServiceResponse<string>
                {
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 204
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> ChangeOrderStatusToDone(int orderId)
        {
            try
            {
                var checkOrderExist = await _orderRepository.GetById(orderId);
                if (checkOrderExist == null)
                {
                    return new ServiceResponse<string>
                    { 
                        Message = "Not found order",
                        Success = true,
                        StatusCode = 200
                    };
                }
                OrderStatus paid = OrderStatus.Paid;
                OrderStatus done = OrderStatus.Done;
                OrderStatus ebook_delivered = OrderStatus.Ebook_delivered;
                if (!checkOrderExist.OrderStatus.Equals(paid.ToString()) && !checkOrderExist.OrderStatus.Equals(ebook_delivered.ToString()))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Can not change status",
                        Success = true,
                        StatusCode = 400
                    };

                }
                else 
                {
                    checkOrderExist.OrderStatus = done.ToString();
                    await _orderRepository.Save();
                }
                return new ServiceResponse<string>
                {
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 204
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> ChangeOrderStatusToPaidWithOCDMethod(int orderId)
        {
            try
            {
                var checkOrderExist = await _orderRepository.GetById(orderId);
                if (checkOrderExist == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Not found order",
                        Success = true,
                        StatusCode = 200
                    };
                }
                OrderStatus accepted = OrderStatus.Accepted;
                OrderStatus paid = OrderStatus.Paid;

                if (checkOrderExist.OrderStatus.Equals(accepted.ToString()))
                {
                    var lstOrderDetailFromOrderId = await _orderDetailRepository.GetByCondition(x => x.OrderId == orderId);
                    if (lstOrderDetailFromOrderId.Count() <= 0)
                    {
                        return new ServiceResponse<string>
                        {
                            Message = "Order details have no rows",
                            Success = true,
                            StatusCode = 400
                        };
                    }
                    else
                    {
                        var list = new List<string>();
                        foreach (var item in lstOrderDetailFromOrderId)
                        {
                            if (item.EbookId != null)
                            {
                                var ebook = await _eBookRepository.GetByWithCondition(x => x.EbookId == item.EbookId, null, true);
                                list.Add(ebook.PdfUrl.ToString());
                            }
                            
                        }
                        var acc = await _accountRepository.GetById(checkOrderExist.CustomerId);
                        EmailModel request = new EmailModel();
                        request.To = acc.Email;
                        request.Subject = "Đơn hàng thanh toán thành công";


                        MailMessage message = new MailMessage();
                        message.From = new MailAddress(_configuration.GetSection("EmailUserName").Value);
                        message.Subject = request.Subject;
                        message.To.Add(new MailAddress(request.To));
                        
                        message.Body = "Chào bạn! Shop J4F trân trọng cảm ơn bạn vì đã mua sản phẩm pdf bên mình, và đây là link sản phẩm: ";
                        foreach (var item in list)
                        {
                            message.Body += item + ", ";
                        }
                        /*message.Body = request.Body;*/
                        message.IsBodyHtml = true;

                        var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value),
                            EnableSsl = true
                        };
                        smtpClient.Send(message);
                    }
                    checkOrderExist.OrderStatus = paid.ToString();
                    await _orderDetailRepository.Save();
                }
                return new ServiceResponse<string>
                { 
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 204
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
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
