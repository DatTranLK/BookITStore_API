using Microsoft.AspNetCore.Mvc;
using Service;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System;
using Service.IServices;
using Entity.Dtos.Publisher;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bookstore_API_JFF.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        [HttpGet(Name = "GetPublishers")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<PublisherDto>>>> GetPublishers([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _publisherService.GetAllPublisherWithPagination(page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("count", Name = "CountPublishers")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountPublishers()
        {
            try
            {
                var res = await _publisherService.CountPublishers();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("pulisher/{id}", Name = "GetPublisherById")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<PublisherDto>>> GetPublisherById(int id)
        {
            try
            {
                var res = await _publisherService.GetPublisherById(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("publisher", Name = "DisableOrEnablePublisher")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> DisableOrEnablePublisher(int id)
        {
            try
            {
                var res = await _publisherService.DisableOrEnablePublisher(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("publisher", Name = "UpdatePublisher")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<Publisher>>> UpdatePublisher(int id, [FromBody] Publisher publisher)
        {
            try
            {
                var res = await _publisherService.UpdatePublisher(id, publisher);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost("publisher", Name = "CreatePublisher")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<Publisher>>> CreatePublisher([FromBody] Publisher publisher)
        {
            try
            {
                var res = await _publisherService.CreatePublisher(publisher);
                return CreatedAtRoute("GetPublisherById", new { id = publisher.Id }, publisher);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("cus", Name = "GetPublishersForCus")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<PublisherDto>>>> GetPublishersForCus([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _publisherService.GetAllPublisherForCusWithPagination(page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("cus/count", Name = "CountPublishersForCus")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountPublishersForCus()
        {
            try
            {
                var res = await _publisherService.CountPublishersForCus();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
