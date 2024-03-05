using Microsoft.AspNetCore.Mvc;
using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SlackApi.Services.RelationRequestService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelationRequestController : ControllerBase
    {
        private readonly IRelationRequestService _relationRequestService;

        public RelationRequestController(IRelationRequestService relationRequestService)
        {
            _relationRequestService = relationRequestService ?? throw new ArgumentNullException(nameof(relationRequestService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRelationRequest([FromBody] RelationRequestDto requestDto)
        {
            try
            {
                var relationRequest = await _relationRequestService.CreateRelationRequest(requestDto);
                return Ok(relationRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRelationRequestsByUserId(long id)
        {
            try
            {
                var relationRequests = await _relationRequestService.GetAllRelationRequestsByUserId(id);
                return Ok(relationRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRelationRequestById(long id)
        {
            try
            {
                var relationRequest = await _relationRequestService.GetRelationRequestById(id);
                if (relationRequest == null)
                {
                    return NotFound();
                }
                return Ok(relationRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("requestor/{id}")]
        public async Task<IActionResult> GetAllRelationRequestsByRequestorId(long id)
        {
            try
            {
                var relationRequests = await _relationRequestService.GetAllRelationRequestsByRequestorId(id);
                return Ok(relationRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRelationRequest(long id, [FromBody] UpdateRelationRequestDto requestDto)
        {
            try
            {
                requestDto.Id = id;
                var result = await _relationRequestService.UpdateRelationRequest(requestDto);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelationRequest(long id)
        {
            try
            {
                var result = await _relationRequestService.DeleteRelationRequest(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("requestor/{id}/receiver/{receiverid}")]
        public async Task<IActionResult> RelationRequestId(long id,long receiverid)
        {

            try
            {
                var relationRequests = await _relationRequestService.GetRelationRequestByReceiverAndRequestor(id,receiverid);
                return Ok(relationRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }



        }




    }
}
