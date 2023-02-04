using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.IServices;
using Service.Services;
using System.Net;
using System.Threading.Tasks;
using System;
using Entity.Dtos.ComboBookDTO;
using Entity.Models;
using Entity.Dtos.DetailComboBookDTO;
using Entity.Dtos.Category;
using System.Collections.Generic;

namespace Bookstore_API_JFF.Controllers
{
    [Route("api/detail-combo-book")]
    [ApiController]
    public class DetailComboBookController : ControllerBase
    {
        private readonly IDetailComboBookService _detailComboBookService;
        public DetailComboBookController(IDetailComboBookService detailComboBookService)
        {
            _detailComboBookService = detailComboBookService;
        }

        [HttpDelete("detail-combo-book", Name = "RemoveDetailComboBook")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<string>>> RemoveDetailComboBook(int id)
        {
            try
            {
                var res = await _detailComboBookService.RemoveDetailComboBook(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("detail-combo-book", Name = "CreateDetailNewComboBook")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<DetailComboBook>>> CreateDetailNewComboBook([FromBody] CreateNewComboDetailRequest createNewComboDetail)
        {
            try
            {
                var res = await _detailComboBookService.CreateNewDetailComboBook(createNewComboDetail.comboId, createNewComboDetail.bookId);
                return CreatedAtRoute("GetDetailComboBookById", new { id = res });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("detail-combo-book/{id}", Name = "GetDetailComboBookById")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<CategoryDto>>> GetDetailComboBookById(int id)
        {
            try
            {
                var res = await _detailComboBookService.GetDetailComboBookyById(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpGet("list-detail-combo-book/combo/{id}", Name = "GetDetailComboBooksOfCombo")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<CategoryDto>>>> GetDetailComboBooksOfCombo(int id)
        {
            try
            {
                var res = await _detailComboBookService.GetListInDetailOfComboBookId(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
