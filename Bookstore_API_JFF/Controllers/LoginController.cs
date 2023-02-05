using Entity;
using Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.IServices;
using System;
using System.Threading.Tasks;

namespace Bookstore_API_JFF.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<string>>> Authentication(IdTokenModel idToken)
        {
            
            try
            {
                var res = await _authenticationService.Authentication(idToken);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
    }
}
