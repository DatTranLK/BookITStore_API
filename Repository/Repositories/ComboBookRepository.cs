using Entity.Models;
using Repository.GenericRepositories;
using Repository.IRepositories;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{

    public class ComboBookRepository : GenericRepository<ComboBook>, IComboBookRepository
    {
        private readonly BookStoreDBAPIContext dbContext;

        public ComboBookRepository(BookStoreDBAPIContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
