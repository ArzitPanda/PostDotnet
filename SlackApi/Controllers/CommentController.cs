using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SocialTree.Data.Dto.RequestDto;
using SocialTree.Data.Model.MongoModel;
using SocialTree.Services.CommentService;

namespace SocialTree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MComment>> GetComment(string id)
        {
            var comment = await _commentService.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }
            return comment;
        }

        [HttpGet("subcomments/{id}")]
        public async Task<ActionResult<List<MSubComment>>> GetSubCommentByComment(string id, int pageSize = 10, int pageNo = 1)
        {
            var subComments = await _commentService.GetSubCommentByComment(id, pageSize, pageNo);
            return subComments;
        }

        [HttpGet("post/{postId}")]
        public async Task<ActionResult<List<MComment>>> GetCommentsByPost(long postId, int pageSize = 10, int pageNo = 1)
        {
            var comments = await _commentService.GetCommentsByPost(postId, pageSize, pageNo);
            return comments;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            var result = await _commentService.DeleteComment(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(string id, MComment updatedComment)
        {
            if (id != updatedComment.Id.ToString())
            {
                return BadRequest();
            }
            var result = await _commentService.UpdateComment(id, updatedComment);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("subcomment/{id}")]
        public async Task<IActionResult> UpdateSubComment(string id, MSubComment updatedSubComment)
        {
            if (id != updatedSubComment.Id.ToString())
            {
                return BadRequest();
            }
            var result = await _commentService.UpdateSubComment(id, updatedSubComment);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("subcomment/{id}")]
        public async Task<IActionResult> DeleteSubComment(string id)
        {
            var result = await _commentService.DeleteSubComment(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> AddCommentToPost([FromBody] MComment comment)
        {
            var success = await _commentService.AddCommentToPost(comment);
            if (success)
            {
                return Ok();
            }
            return StatusCode(500); // Internal Server Error
        }

        [HttpPost("subcomment/{parentId}")]
        public async Task<IActionResult> AddSubCommentToAComment(string parentId, [FromBody] SubCommentDto subComment)
        {
            if (!ObjectId.TryParse(parentId, out var objectId))
            {
                return BadRequest("Invalid parentId");
            }

            var success = await _commentService.AddSubCommentToAComment(objectId, subComment);
            if (success)
            {
                return Ok();
            }
            return StatusCode(500); // Internal Server Error
        }



    }
}
