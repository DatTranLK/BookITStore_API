using Entity.Dtos.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.IServices;
using Service.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Net;
using Entity.Models;
using Entity.Dtos.ComboBookDTO;

namespace Bookstore_API_JFF.Controllers
{
    [Route("api/combobooks")]
    [ApiController]
    public class ComboBookController : ControllerBase
    {
        private readonly IComboBookService comboBookService;

        public ComboBookController(IComboBookService comboBookService)
        {
            this.comboBookService = comboBookService;
        }

        [HttpGet(Name = "GetComboBooks")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<CategoryDto>>>> GetCategories([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await comboBookService.GetComboBookWithPagination(page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("count", Name = "CountComboBooks")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountCategories()
        {
            try
            {
                var res = await comboBookService.CountComboBooks();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("combobook/{id}", Name = "GetComboBookById")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<CategoryDto>>> GetCategoryById(int id)
        {
            try
            {
                var res = await comboBookService.GetComboBookyById(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("combobook", Name = "DisableOrEnableComboBook")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> DisableOrEnableCategory(int id)
        {
            try
            {
                var res = await comboBookService.DisableOrEnableComboBook(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("combobook", Name = "UpdateComboBook")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<ComboBook>>> UpdateComboBook([FromBody] ComboBook comboBook)
        {
            try
            {
                var res = await comboBookService.UpdateComboBook(comboBook);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("combobook", Name = "CreateNewComboBook")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<int>>> CreateNewComboBook([FromBody] CreateComboRequest createComboRequest)
        {
            try
            {
                var res = await comboBookService.CreateNewComboBook(createComboRequest.comboBook, createComboRequest.bookId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
