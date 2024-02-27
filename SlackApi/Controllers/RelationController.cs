using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlackApi.Data.Dto.RequestDto;
using SlackApi.Services.RelationService;

namespace SlackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationController : ControllerBase
    {
        private readonly IRelationService _relationService;

        public RelationController(IRelationService relationService)
        {
            _relationService = relationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRelation([FromBody] RelationCreateDto relationDto)
        {
            try
            {
                var createdRelation = await _relationService.CreateRelation(relationDto);
                return Ok(createdRelation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{relationId}")]
        public async Task<IActionResult> GetRelationById(long relationId)
        {
            try
            {
                var relation = await _relationService.GetRelationById(relationId);
                if (relation == null)
                    return NotFound("Relation not found");

                return Ok(relation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("sender/{senderId}")]
        public async Task<IActionResult> GetRelationsBySenderId(long senderId)
        {
            try
            {
                var relations = await _relationService.GetRelationsBySenderId(senderId);
                return Ok(relations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("receiver/{receiverId}")]
        public async Task<IActionResult> GetRelationsByReceiverId(long receiverId)
        {
            try
            {
                var relations = await _relationService.GetRelationsByReceiverId(receiverId);
                return Ok(relations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut]
        public async  Task<IActionResult> UpdateRealtionById(long id,string type)
        {

            try
            {
                var relation = await _relationService.UpdateRelationById(id, type);
                return Ok("sucessfully updated");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }






    }
}
