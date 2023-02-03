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
    public class DetailComboBookRepository : GenericRepository<DetailComboBook>, IDetailComboBookRepository
    {
        private readonly BookStoreDBAPIContext dbContext;

        public DetailComboBookRepository(BookStoreDBAPIContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
