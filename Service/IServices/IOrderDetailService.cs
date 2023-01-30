using Entity.Dtos.OrderDetail;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IOrderDetailService
    {
        Task<ServiceResponse<IEnumerable<OrderDetailDto>>> GetOrderDetailsById(int orderId);
        Task<ServiceResponse<int>> CountAll(int orderId);
        Task<ServiceResponse<int>> CreateOrderDetail(OrderDetail orderDetail);
        Task<ServiceResponse<OrderDetail>> UpdateOrderDetail(int id, OrderDetail orderDetail);
    }
}
