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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly BookStoreDBAPIContext _dbContext;

        public OrderRepository(BookStoreDBAPIContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
