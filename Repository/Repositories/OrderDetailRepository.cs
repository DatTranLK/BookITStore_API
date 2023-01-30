using Entity.Models;
using Repository.GenericRepositories;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly BookStoreDBAPIContext _dbContext;

        public OrderDetailRepository(BookStoreDBAPIContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
