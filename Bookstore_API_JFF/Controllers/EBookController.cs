﻿using Entity.Dtos.EBook;
using Entity.Models;
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

        public EBookController(IEBookService eBookService)
        {
            _eBookService = eBookService;
        }
        [HttpGet(Name = "GetEBooks")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<EBookDto>>>> GetEBooks([FromQuery] int page, [FromQuery] int pageSize)
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
        public async Task<ActionResult<ServiceResponse<EBookDto>>> GetEBookById(int id)
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
        [HttpGet("ebook/book/{bookId}", Name = "GetEBookByBookId")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<EBookDto>>> GetEBookByBookId(int bookId)
        {
            try
            {
                var res = await _eBookService.GetEBookByBookId(bookId);
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
        [HttpPost("ebook", Name = "CreateNewEBook")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<int>>> CreateNewEBook([FromBody]EBookDto eBookDto)
        {
            try
            {
                var res = await _eBookService.CreateNewEBook(eBookDto);
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
    }
}
