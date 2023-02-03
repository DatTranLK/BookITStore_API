using Entity.Dtos.Order;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IOrderService
    {
        //For Admin
        Task<ServiceResponse<IEnumerable<OrderDtoForAdmin>>> GetOrdersForAdminWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountOrdersForAdmin();
        Task<ServiceResponse<OrderDtoForAdmin>> GetOrderByIdForAdmin(int orderId);
        //For Customer
        Task<ServiceResponse<IEnumerable<OrderDtoForCus>>> GetOrdersForCusWithPagination(int cusId, int page, int pageSize);
        Task<ServiceResponse<int>> CountOrdersForCus(int cusId);
        Task<ServiceResponse<OrderDtoForCus>> GetOrderByIdForCus(int orderId);

        Task<ServiceResponse<int>> CreateNewOrder(Order order);
    }
}
