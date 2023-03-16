using Entity.Dtos.BookImage;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.IServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Bookstore_API_JFF.Controllers
{
    [Route("api/book-images")]
    [ApiController]
    public class BookImageController : ControllerBase
    {
        private readonly IBookImageService _bookImageService;

        public BookImageController(IBookImageService bookImageService)
        {
            _bookImageService = bookImageService;
        }
        [HttpGet(Name = "GetBookImages")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<BookImageDto>>>> GetBookImages([FromQuery]int page, [FromQuery]int pageSize)
        {
            try
            {
                var res = await _bookImageService.GetBookImageWithPagination(page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("count", Name = "CountBookImages")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountBookImages()
        {
            try
            {
                var res = await _bookImageService.CountBookImages();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("book-image/book/{bookId}", Name = "GetBookImageByBookId")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<BookImageDto>>>> GetBookImageByBookId(int bookId)
        {
            try
            {
                var res = await _bookImageService.GetBookImageByBookId(bookId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message); ;
            }
        }

		[HttpGet("book-image/ebook/{ebookId}", Name = "GetBookImageByEBookId")]
		[Produces("application/json")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<ActionResult<ServiceResponse<IEnumerable<BookImageDto>>>> GetBookImageByEBookId(int ebookId)
		{
			try
			{
				var res = await _bookImageService.GetBookImageByEBookId(ebookId);
				return StatusCode((int)res.StatusCode, res);
			}
			catch (Exception ex)
			{

				return StatusCode(500, "Internal server error: " + ex.Message); ;
			}
		}

		[HttpGet("book-image/{bookImageId}", Name = "GetBookImageById")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<BookImageDto>>> GetBookImageById(int bookImageId)
        {
            try
            {
                var res = await _bookImageService.GetBookImageById(bookImageId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message); ;
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("book-image", Name = "UpdateBookimage")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<BookImage>>> UpdateBookimage(int id, [FromBody] BookImage bookImage)
        {
            try
            {
                var res = await _bookImageService.UpdateBookImage(id, bookImage);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message); ;
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost("book-image", Name = "CreateBookImage")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<BookImage>>> CreateBookImage([FromBody] BookImage bookImage)
        {
            try
            {
                var res = await _bookImageService.CreateNewBookImage(bookImage);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message); ;
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost("book-image/ebook", Name = "CreateEBookImage")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<BookImage>>> CreateEBookImage([FromBody] BookImage bookImage)
        {
            try
            {
                var res = await _bookImageService.CreateNewEBookImage(bookImage);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message); ;
            }
        }
    }
}
