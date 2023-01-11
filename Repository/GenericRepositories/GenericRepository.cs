using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public Task<ServiceResponse<string>> Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<IEnumerable<T>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<IEnumerable<T>>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<T>> GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<int>> Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<string>> Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
