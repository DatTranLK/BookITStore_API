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
        private readonly db_a956d7_bookstoredbContext dbContext;

        public DetailComboBookRepository(db_a956d7_bookstoredbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
