using Entity.Dtos.Book;
using Entity.Dtos.EBook;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Bookstore_API_JFF.Controllers
{
    [Route("api/ebooks")]
    [ApiController]
    public class EBookController : ControllerBase
    {
        private readonly IEBookService _eBookService;
        private readonly IBookService _bookService;

        public EBookController(IEBookService eBookService, IBookService bookService)
        {
            _eBookService = eBookService;
            _bookService = bookService;
        }
        [HttpGet(Name = "GetEBooks")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Ebook>>>> GetEBooks([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _eBookService.GetEBooksWithPagination(page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpGet("count", Name = "CountEBooks")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountEBooks()
        {
            try
            {
                var res = await _eBookService.CountAll();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("ebook/{id}", Name = "GetEBookById")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<EBookDetailDto>>> GetEBookById(int id)
        {
            try
            {
                var res = await _eBookService.GetEBookById(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        /// <summary>
        /// Add New EBook
        /// </summary>
        ///
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost("ebook", Name = "CreateNewEBook")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<int>>> CreateNewEBook([FromBody] Ebook ebook)
        {
            try
            {
                var res = await _eBookService.CreateNewEBook(ebook);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        /// <summary>
        /// Update EBook
        /// </summary>
        ///
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("ebook", Name = "UpdateEBook")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> UpdateEBook(int id, [FromBody] EBookDtoForUpdate eBookDtoForUpdate)
        {
            try
            {
                var res = await _eBookService.ChangeInformationOfEBook(id, eBookDtoForUpdate);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("cus/top-ebooks", Name = "GetEBooksShowVer2WithPagination")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<EBookShowDtoVer2>>>> GetEBooksShowVer2WithPagination([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _eBookService.GetEBookForCusWithPagination(page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("cus/top-ebooks/count", Name = "CountEBooksShowVer2WithPagination")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountEBooksShowVer2WithPagination()
        {
            try
            {
                var res = await _eBookService.CountEBooksForCus();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("admin/e-books", Name = "GetEBookWithPagination")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<EBookDtoForAdmin>>>> GetEBookWithPagination([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _eBookService.GetEBookWithPagination(page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("admin/e-books/count", Name = "CountEBooksForAdmin")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountEBooksForAdmin()
        {
            try
            {
                var res = await _eBookService.CountEBooks();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
