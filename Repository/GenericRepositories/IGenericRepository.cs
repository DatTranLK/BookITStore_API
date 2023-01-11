using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.GenericRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ServiceResponse<IEnumerable<T>>> GetAll();
        Task<ServiceResponse<T>> GetById(object id);
        Task<ServiceResponse<IEnumerable<T>>> GetByCondition(Expression<Func<T, bool>> expression);
        Task<ServiceResponse<int>> Insert(T obj);
        Task<ServiceResponse<string>> Delete(object obj);
        Task<ServiceResponse<string>> Update(T obj);
    }
}
