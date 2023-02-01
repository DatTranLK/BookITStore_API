using Entity.Models;
using Repository.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
    }
}
