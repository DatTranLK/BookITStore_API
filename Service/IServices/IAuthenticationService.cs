using Entity;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<string>> Authentication(IdTokenModel idToken);
    }
}
