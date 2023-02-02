using Entity.Dtos.Account;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IAccountService
    {
        Task<ServiceResponse<IEnumerable<AccountDto>>> GetAllAcoountWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountAccounts();
        Task<ServiceResponse<AccountDto>> GetAccountById(int id);
        Task<ServiceResponse<string>> DisableOrEnableAccount(int id);
    }
}
