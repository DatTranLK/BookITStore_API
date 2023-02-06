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
using Entity.Dtos.Book;

namespace Bookstore_API_JFF.Controllers
{
    [Route("api/detail-combo-books")]
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
        public async Task<ActionResult<ServiceResponse<DetailComboBook>>> CreateDetailNewComboBook([FromBody] CreateNewComboDetailRequest createNewComboDetail)
        {
            try
            {
                var res = await _detailComboBookService.CreateNewDetailComboBook(createNewComboDetail.comboId, createNewComboDetail.bookId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpGet("books-of-combo/{comboId}", Name = "GetDetailComboBooksOfCombo")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ListBookOfCombo>>>> GetDetailComboBooksOfCombo(int comboId)
        {
            try
            {
                var res = await _detailComboBookService.GetListInDetailOfComboBookId(comboId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
