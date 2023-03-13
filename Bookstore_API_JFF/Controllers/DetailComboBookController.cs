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

        [HttpPost("admin/detail-combo-book", Name = "CreateDetailNewComboBookForAdmin")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateDetailNewComboBookForAdmin([FromBody]DetailComboBook detailComboBook)
        {
            try
            {
                var res = await _detailComboBookService.CreateNewDetailComboPhysicalBookVer2(detailComboBook);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("admin/detail-combo-ebook", Name = "CreateDetailNewComboEBookForAdmin")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateDetailNewComboEBookForAdmin([FromBody] DetailComboBook detailComboBook)
        {
            try
            {
                var res = await _detailComboBookService.CreateNewDetailComboEBookVer2(detailComboBook);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpGet("books-of-combo/{comboId}", Name = "GetDetailComboBooksPhysicalOfCombo")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ListPhysicalBookOfCombo>>>> GetDetailComboBooksPhysicalOfCombo(int comboId)
        {
            try
            {
                var res = await _detailComboBookService.GetListInDetailPhysicalBookOfComboBookId(comboId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        

        [HttpGet("admin/books-of-combo/{comboId}", Name = "GetComboBooksOfComboWithPagination")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<DetailComboBookDtoShow>>>> GetComboBooksOfComboWithPagination(int comboId, [FromQuery]int page, [FromQuery]int pageSize)
        {
            try
            {
                var res = await _detailComboBookService.GetDetailOfComboBookIdWithPagination(comboId, page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("admin/ebooks-of-combo/{comboId}", Name = "GetComboEBooksOfComboWithPagination")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<DetailComboEBookDtoShow>>>> GetComboEBooksOfComboWithPagination(int comboId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _detailComboBookService.GetDetailOfComboEBookIdWithPagination(comboId, page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
