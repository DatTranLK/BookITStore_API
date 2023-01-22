using Entity;
using Entity.Models;
using Repository.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IAuthenticationRepository : IGenericRepository<Account>
    {
        Task<Account> Authentication(IdTokenModel idToken);
    }
}
