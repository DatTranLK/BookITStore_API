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
        private readonly db_a956d7_bookstoredbContext dbContext;

        public ComboBookRepository(db_a956d7_bookstoredbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
