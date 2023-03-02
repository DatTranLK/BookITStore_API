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
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly db_a956d7_bookstoredbContext _dbContext;

        public AccountRepository(db_a956d7_bookstoredbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
