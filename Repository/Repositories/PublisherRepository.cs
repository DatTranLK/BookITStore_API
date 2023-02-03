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
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        private readonly BookStoreDBAPIContext _dbContext;

        public PublisherRepository(BookStoreDBAPIContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
