using AutoMapper;
using Entity;
using Entity.Dtos.Order;
using Entity.Enum;
using Entity.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
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
        private readonly IVnPayService _vnPayService;
        private readonly IComboBookRepository _comboBookRepository;
        private readonly IDetailComboBookRepository _detailComboBookRepository;
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IEBookRepository _eBookRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public OrderService(IOrderRepository orderRepository, IVnPayService vnPayService, IComboBookRepository comboBookRepository, IDetailComboBookRepository detailComboBookRepository, IConfiguration configuration, IAccountRepository accountRepository, IOrderDetailRepository orderDetailRepository, IBookRepository bookRepository, IEBookRepository eBookRepository)
        {
            _orderRepository = orderRepository;
            _vnPayService = vnPayService;
            _comboBookRepository = comboBookRepository;
            _detailComboBookRepository = detailComboBookRepository;
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
                                var lstdetailComboBook = await _detailComboBookRepository.GetByCondition(x => x.ComboBookId == item.ComboBookId);
                                foreach (var itemInList in lstdetailComboBook)
                                {
                                    var getBookByBookId = await _bookRepository.GetById(itemInList.BookId);
                                    getBookByBookId.Amount -= item.Quantity;
                                    getBookByBookId.AmountSold += item.Quantity;
                                    await _bookRepository.Save();
                                }
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
                OrderStatus cancel = OrderStatus.Cancel;
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
                        /*EmailModel request = new EmailModel();
                        request.To = acc.Email;
                        request.Subject = "Đơn hàng thanh toán thành công";*/

                        var email = new MimeMessage();
                        email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
                        email.To.Add(MailboxAddress.Parse(acc.Email));
                        email.Subject = "Đơn hàng thanh toán thành công";
                        /*email.Body = new TextPart(TextFormat.Html)
                        {
                            Text = "Chào bạn! Shop J4F trân trọng cảm ơn bạn vì đã mua sản phẩm pdf bên mình, và đây là link sản phẩm: "
                        };*/
                        string x = "Chào bạn! Shop J4F trân trọng cảm ơn bạn vì đã mua sản phẩm pdf bên mình, và đây là link sản phẩm: ";
                        foreach (var item in list)
                        {
                            x += item + ", ";
                        }
                        email.Body = new TextPart(TextFormat.Html)
                        {
                            Text = x
                        };

                        using var smtp = new MailKit.Net.Smtp.SmtpClient();
                        smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                        smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
                        smtp.Send(email);
                        smtp.Disconnect(true);


                        /*MailMessage message = new MailMessage();
                        message.From = new MailAddress(_configuration.GetSection("EmailUserName").Value);
                        message.Subject = request.Subject;
                        message.To.Add(new MailAddress(request.To));
                        
                        message.Body = "Chào bạn! Shop J4F trân trọng cảm ơn bạn vì đã mua sản phẩm pdf bên mình, và đây là link sản phẩm: ";
                        foreach (var item in list)
                        {
                            message.Body += item + ", ";
                        }
                        message.IsBodyHtml = true;

                        var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value),
                            EnableSsl = true
                        };
                        smtpClient.Send(message);*/
                    }
                    checkOrderExist.OrderStatus = paid.ToString();
                    await _orderDetailRepository.Save();
                }
                else 
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Can not change status",
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

        public async Task<ServiceResponse<string>> CheckingPaidWithOlinePaymentMethod(int orderId)
        {
            try
            {
                OrderStatus processing = OrderStatus.In_Progress;
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
                if (!checkOrderExist.OrderStatus.Equals(processing.ToString()))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Can not change status",
                        Success = true,
                        StatusCode = 400
                    };
                }
                OrderStatus paid = OrderStatus.Paid;
                OrderStatus Ebook_delivered = OrderStatus.Ebook_delivered;
                var lstOrderDetailByOrderId = await _orderDetailRepository.GetByCondition(x => x.OrderId == orderId);
                if (lstOrderDetailByOrderId.Count() <= 0)
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
                    foreach (var item in lstOrderDetailByOrderId)
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
                            list.Add(ebook.PdfUrl.ToString());
                        }
                        else if (item.ComboBookId != null)
                        {
                            var lstdetailComboBook = await _detailComboBookRepository.GetByCondition(x => x.ComboBookId == item.ComboBookId);
                            foreach (var itemInList in lstdetailComboBook)
                            {
                                var getBookByBookId = await _bookRepository.GetById(itemInList.BookId);
                                getBookByBookId.Amount -= item.Quantity;
                                getBookByBookId.AmountSold += item.Quantity;
                                await _bookRepository.Save();
                            }
                        }
                    }
                    if (list.Count() <= 0)
                    {
                        checkOrderExist.OrderStatus = paid.ToString();
                        await _orderRepository.Save();
                    }
                    else 
                    {
                        

                        var acc = await _accountRepository.GetById(checkOrderExist.CustomerId);
                        EmailModel request = new EmailModel();
                        request.To = acc.Email;
                        request.Subject = "Đơn hàng thanh toán thành công";

                        var email = new MimeMessage();
                        email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
                        email.To.Add(MailboxAddress.Parse(acc.Email));
                        email.Subject = "Đơn hàng thanh toán thành công";
                        /*email.Body = new TextPart(TextFormat.Html)
                        {
                            Text = "Chào bạn! Shop J4F trân trọng cảm ơn bạn vì đã mua sản phẩm pdf bên mình, và đây là link sản phẩm: "
                        };*/
                        string x = "Chào bạn! Shop J4F trân trọng cảm ơn bạn vì đã mua sản phẩm pdf bên mình, và đây là link sản phẩm: ";
                        foreach (var item in list)
                        {
                            x += item + ", ";
                        }
                        email.Body = new TextPart(TextFormat.Html)
                        { 
                            Text = x
                        };

                        using var smtp = new MailKit.Net.Smtp.SmtpClient();
                        smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                        smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
                        smtp.Send(email);
                        smtp.Disconnect(true);

                        /*MailMessage message = new MailMessage();
                        message.From = new MailAddress(_configuration.GetSection("EmailUserName").Value);
                        message.Subject = request.Subject;
                        message.To.Add(new MailAddress(request.To));

                        message.Body = "Chào bạn! Shop J4F trân trọng cảm ơn bạn vì đã mua sản phẩm pdf bên mình, và đây là link sản phẩm: ";
                        foreach (var item in list)
                        {
                            message.Body += item + ", ";
                        }
                        
                        message.IsBodyHtml = true;

                        var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value),
                            EnableSsl = true
                        };
                        smtpClient.Send(message);*/
                        checkOrderExist.OrderStatus = Ebook_delivered.ToString();
                        await _orderRepository.Save();
                    }
                    
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

        public async Task<ServiceResponse<string>> CreateNewOrderWithOnlinePayment(Order order, HttpContext context)
        {
            try
            {
                //validation in here
                //starting insert into Db
                

                OrderStatus processing = OrderStatus.In_Progress;
                order.CreateDate = DateTime.Now;
                order.PaymentMethod = "Thanh toán online";
                order.OrderStatus = processing.ToString();
                await _orderRepository.Insert(order);
                
                var url = _vnPayService.CreatePaymentUrl(order, context);

                return new ServiceResponse<string>
                {
                    Data = url,
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
